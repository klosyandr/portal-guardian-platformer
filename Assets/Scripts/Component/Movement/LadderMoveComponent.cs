using System;
using PortalGuardian.Component.ColliderBase;
using PortalGuardian.Component.Props;
using UnityEngine;

namespace PortalGuardian.Creatures
{
    public class LadderMoveComponent : IMoveComponent
    {        
        [Header("Checked")]
        [SerializeField] private LayerCheck _ladderCheck;

        private bool _isOnLadder;
        private float _ladderUp;
        private float _ladderDown;
        private float _ladderCenterHorizontal;

        public Action<bool> ChangeOnLadderStatus;

        private void FixedUpdate()
        {  
            if (!_ladderCheck.IsTouchingLayer) return;

            if (_direction.y != 0) CheckOnLadder();

            if (_isOnLadder &&_direction.x != 0) LadderModeSwitch(false);
        }

        public override void Move()
        {
            transform.Translate(new Vector2(0, _speed * _direction.y * Time.fixedDeltaTime));

            float xPos = Mathf.Lerp(transform.position.x, _ladderCenterHorizontal, 10 * Time.deltaTime);
            transform.position = new Vector2(xPos, transform.position.y);
            OnMoved?.Invoke();
        }
        
        public void SetPointLadder(GameObject go)
        {
            var ladder = go?.GetComponent<LadderComponent>();
            if (ladder == null) return;

            _ladderUp = ladder.Up;
            _ladderDown = ladder.Down;
            _ladderCenterHorizontal = ladder.CenterHorizontal;
        }

        private void CheckOnLadder()
        {            
            var vertical = _direction.y;
            if (_isOnLadder)
            {
                if ((vertical > 0 && transform.position.y > _ladderUp) || 
                    (vertical < 0 && transform.position.y < _ladderDown))
                {  
                        LadderModeSwitch(false);          
                        return;
                }
            } 
            else
            {
                if ((vertical > 0 && transform.position.y < _ladderUp) || 
                    (vertical < 0 && transform.position.y > _ladderUp))
                {
                        LadderModeSwitch(true);
                        return;
                }
            }
        }

        public void LadderModeSwitch(bool state)
        {
            if (state)
            {
                _rigidbody.velocity = Vector2.zero;
                _direction = Vector2.zero;
            }
           
            _rigidbody.isKinematic = state;
            _isOnLadder = state;
            ChangeOnLadderStatus?.Invoke(state);
        }

    }

}

