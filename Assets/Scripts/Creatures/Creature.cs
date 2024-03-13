using PortalGuardian.Component.Audio;
using PortalGuardian.Component.ColliderBase;
using PortalGuardian.Component.GoBased;
using UnityEngine;

namespace PortalGuardian.Creatures{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] protected float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [Header("Checked")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] protected CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D _rigidbody;
        protected Animator _animator;
        protected bool _isGrounded; 
        protected bool _isJumping;
        protected Vector2 _direction; 
        protected PlaySoundsComponent Sounds;
        
        protected static readonly int isGroundKey = Animator.StringToHash("is-ground");
        protected static readonly int yVelocityKey = Animator.StringToHash("y-velocity");
        protected static readonly int isRunKey = Animator.StringToHash("is-running");
        protected static readonly int hitKey = Animator.StringToHash("hit");
        protected static readonly int attackKey = Animator.StringToHash("attack");

        protected virtual void Awake(){
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();    
            Sounds = GetComponent<PlaySoundsComponent>();         
        }

        protected virtual void Update(){
            _isGrounded = _groundCheck.IsTouchingLayer;
        }
        
        public void SetDirection(Vector2 direction){
            _direction = direction;
        }

        protected virtual void FixedUpdate(){
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        
            _animator.SetBool(isGroundKey, _isGrounded);
            _animator.SetBool(isRunKey, _direction.x !=0);
            _animator.SetFloat(yVelocityKey, _rigidbody.velocity.y);

            UpdateSpriteDirection();
        }

        protected virtual float CalculateYVelocity(){
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;
            
            if (_isGrounded) {
                _isJumping = false;
            }

            if (isJumpPressing){
                _isJumping = true;

                var isFalling = _rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity){
            if(_isGrounded){
                yVelocity = _jumpSpeed;
                _direction.y = 0;
                PlayEffects("Jump");
            }
            return yVelocity; 
        }
        
        protected void UpdateSpriteDirection(){
            if (_direction.x > 0) {
                transform.localScale = Vector3.one;
            } else if(_direction.x < 0){
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        protected virtual void TakeDamage(){
            _isJumping = false;
            _animator.SetTrigger(hitKey);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageVelocity);
        }

        public virtual void Attack(){
            _animator.SetTrigger(attackKey);
            PlayEffects("Range");
        }

        public void OnDoAttack(){
            _attackRange.Check();
        }

        protected void PlayEffects(string id){
            _particles.Spawn(id);
            Sounds.Play(id);
        }

    }

}

