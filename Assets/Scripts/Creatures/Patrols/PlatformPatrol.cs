using System.Collections;
using PortalGuardian.Component.ColliderBase;
using UnityEngine;

namespace PortalGuardian.Creatures.Patrols{
    public class PlatformPatrol : Patrol
    {
        
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private Vector2 _direction;

        private Creature _creature;

        private void Awake(){
            _creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled){
                if(!_groundCheck.IsTouchingLayer){
                    _direction.x *= -1;
                }                      
                _direction.y = 0;          
                _creature.SetDirection(_direction.normalized);
                yield return null;
            }
        }
    }
}