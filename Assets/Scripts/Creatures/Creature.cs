using PortalGuardian.Component.Audio;
using PortalGuardian.Component.ColliderBase;
using PortalGuardian.Component.GoBased;
using PortalGuardian.Model.Data;
using UnityEngine;

namespace PortalGuardian.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float _damageVelocity;

        [Header("Checked")]
        [SerializeField] protected CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected IMoveComponent _tempMoveMode;
        protected Rigidbody2D _rigidbody;
        protected Animator _animator;
        protected PlaySoundsComponent _sounds;
        
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();    
            _sounds = GetComponent<PlaySoundsComponent>();
            
            _tempMoveMode = GetComponent<IMoveComponent>();
        }
        
        public virtual void SetDirection(Vector2 direction)
        {
            if(_tempMoveMode == null) return;

            _tempMoveMode.Direction = direction;
        }

        protected virtual void FixedUpdate()
        {
            _tempMoveMode?.Move();
        }

        protected virtual void TakeDamage()
        {
            _animator.SetTrigger(AnimatorKeys.HIT);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageVelocity);
        }

        public virtual void Attack()
        {
            _animator.SetTrigger(AnimatorKeys.ATTACK);
            PlayEffects(Keys.RANGE);
        }

        public void OnDoAttack()
        {
            _attackRange.Check();
        }

        protected void PlayEffects(string id)
        {
            _particles.Spawn(id);
            _sounds.Play(id);
        }

    }

}

