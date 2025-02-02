using PortalGuardian.Component.GoBased;
using PortalGuardian.Model.Data;
using PortalGuardian.Utils;
using UnityEngine;

namespace PortalGuardian.Creatures.Player
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private SpawnListComponent _particles;
        [Header("Move")]
        [SerializeField] private MoveComponent _simpleMove;
        [SerializeField] private LadderMoveComponent _ladderMove;
        [SerializeField] private float _fallVelocity;
        [Header("Checked")]
        [SerializeField] private LayerMask _groundLayer;

        private IMoveComponent _tempMoveMode;
        private Animator _animator;        
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();            
            _tempMoveMode = _simpleMove;

            _simpleMove.OnJump += () => PlayEffects(EffectsKeys.JUMP);
            _simpleMove.OnMoved += PlayAnimationSimpleMove;

            _ladderMove.ChangeOnLadderStatus += ChangeOnLadderStatus;
            _ladderMove.OnMoved += PlayAnimationLadderMove;
            
        }

        private void FixedUpdate()
        {
            _tempMoveMode.Move();
        }

        public void SetDirection(Vector2 direction)
        {
            _simpleMove.Direction = direction;
            _ladderMove.Direction = direction;
        }

        private void ChangeOnLadderStatus(bool state)
        {
            _tempMoveMode = state ? _ladderMove : _simpleMove;
                     
            _animator.SetBool(AnimatorKeys.CLIMB, state);              
            _animator.SetBool(AnimatorKeys.CLIMBING, false); 
        }

        private void PlayAnimationSimpleMove()
        {
            _animator.SetBool(AnimatorKeys.IS_GROUND, _simpleMove.IsGrounded);
            _animator.SetBool(AnimatorKeys.RUN, _simpleMove.Direction.x != 0);
            _animator.SetFloat(AnimatorKeys.Y_VELOCITY, _rigidbody.velocity.y);
        }
        
        private void PlayAnimationLadderMove()
        {
            _animator.SetBool(AnimatorKeys.CLIMBING, _ladderMove.Direction.y != 0);
        }
        
        private void PlayEffects(string id)
        {
            _particles.Spawn(id);
            //_sounds.Play(id);
        }
        
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _fallVelocity)
                {
                    PlayEffects(EffectsKeys.FALL);
                }
            }
        }     
    }
}