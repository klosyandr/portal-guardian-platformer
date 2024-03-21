using System.Collections;
using PortalGuardian.Component.Health;
using PortalGuardian.Component.ColliderBase;
using PortalGuardian.Component.Props;
using PortalGuardian.Model;
using PortalGuardian.Model.Data;
using PortalGuardian.Utils;
using UnityEditor;
using UnityEngine;
using PortalGuardian.Component.GoBased;
using PortalGuardian.Model.Definitions;

namespace PortalGuardian.Creatures.Player
{
    public class Player : Creature
    {
        [SerializeField] private float _fallVelocity;
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _ladderCheck;
          
        [SerializeField] private Cooldown _throwAttackCooldown;
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private int _coinsToDispose = 5;
        [SerializeField] private SpawnComponent _throwSpawner;

        [Space] [Header("For Gizmos")]
        [SerializeField] private Vector3 _groundCheckPositionDelta;
        [SerializeField] private float _groundCheckRadius;
             
        private bool _allowDoubleJump;
        private bool _isOnLadder;         
        private float _ladderUp;
        private float _ladderDown;        
        private float _ladderCenterHorizontal;        
        private GameSession _session;
        
        protected static readonly int _climbKey = Animator.StringToHash("is-climb");
        protected static readonly int _climbingKey = Animator.StringToHash("is-climbing");     

        private int CoinsCount => _session.Data.Inventory.Count("Coin");        
        private int ChargeCount => _session.Data.Inventory.Count("Charge");
        private InventoryData Inventory => _session.Data.Inventory;
        private string SelectedItemId =>_session.QuickInvenory.SelectedItem.Id;

        public void Start()
        {
            _session = FindObjectOfType<GameSession>();

            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data.Hp.Value);
        }

        protected override void FixedUpdate()
        {  
            if (_ladderCheck.IsTouchingLayer && _direction.y != 0) CheckLadder();    

            if (_isOnLadder){ 
                if (_direction.x == 0){  
                    MoveLadderMode();
                    return;
                }           
                LadderModeSwitch(false);
            } 
            
            MoveSimple();
        }

        public void CheckLadder()
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
            _animator.SetBool(_climbKey, state);              
            _animator.SetBool(_climbingKey, false);                 
        }

        private void MoveLadderMode()
        {
            transform.Translate(new Vector2(0, _speed * _direction.y * Time.fixedDeltaTime));
            _animator.SetBool(_climbingKey, _direction.y != 0);

            float xPos = Mathf.Lerp(transform.position.x, _ladderCenterHorizontal, 10 * Time.deltaTime);
            transform.position = new Vector2(xPos, transform.position.y);
        }

        public void GetPointLadder(GameObject go)
        {
            var ladder = go.GetComponent<LadderComponent>();
            if (ladder != null)
            {
                _ladderUp = ladder.Up;
                _ladderDown = ladder.Down;
                _ladderCenterHorizontal = ladder.CenterHorizontal;
            }
        }

        private void MoveSimple()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        
            _animator.SetBool(isGroundKey, _isGrounded);
            _animator.SetBool(isRunKey, _direction.x !=0);
            _animator.SetFloat(yVelocityKey, _rigidbody.velocity.y);

            UpdateSpriteDirection();
        }

        protected override float CalculateYVelocity()
        {
            if (_isGrounded) 
            {
                _allowDoubleJump = true;
            }
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if(!_isGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;
                _direction.y = 0; 
                PlayEffects("Jump"); 
                return _jumpSpeed;
            }    
            return base.CalculateJumpVelocity(yVelocity);      
        }

        protected override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public void DoThrowAttack()
        {
           if (CheckThrowAttack()) ThrowAndRemoveFromInventory();
        }    

        public void DoThrowAttackSeries()
        { 
            if (CheckThrowAttack()) StartCoroutine(ThrowAttackSeries());
        }        

        private void ThrowAndRemoveFromInventory()
        {
            _throwAttackCooldown.Reset();

            var trowableId = _session.QuickInvenory.SelectedItem.Id;
            var trowableDef = DefsFacade.I.TrowableItems.Get(trowableId);

            _throwSpawner.SetPrefab(trowableDef.Projectile);
            PlayEffects("Melee");
            Inventory.Remove(trowableId, 1);
        }

        private bool CheckThrowAttack()
        {
            var def = DefsFacade.I.Items.Get(SelectedItemId);
            if (!_session.Data._hasMelee) return false;
            if (!_throwAttackCooldown.IsReady) return false;
            
            return def.HasTag(ItemTag.Throwable);
        }
        
        private IEnumerator ThrowAttackSeries ()
        {
            int deltaCharge = 0;
            while (ChargeCount > 0 && deltaCharge != 3)
            {
                yield return new WaitForSeconds(0.5f);
                ThrowAndRemoveFromInventory();
                deltaCharge ++;
            }
        }

        public void RangeAttack()
        {
            if (!_session.Data._hasRange) return;

            PlayEffects("Range");
            _attackRange.Check();
        }

        public void SpawnCoins()
        {
            var score = CoinsCount;
            var value = Mathf.Min(_coinsToDispose, score);
            Inventory.Remove("Coin", value);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = value;
            _hitParticles.emission.SetBurst(0,burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        public void Heal()
        {
            if (Inventory.Count("Potion") == 0) return;

            _session.Data.Hp.Value += 5;
            Inventory.Remove("Potion", 1);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer)){
                var contact = other.contacts[0];
                var x = contact.relativeVelocity.y;
                if (contact.relativeVelocity.y >= _fallVelocity){
                    PlayEffects("Fall");
                }
            }
        }       

        public void NextItem()
        {
            _session.QuickInvenory.SetNextItem();
        } 

        public void OnChangeHealth(int newValue)
        {
            _session.Data.Hp.Value =  newValue;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = _isGrounded ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadius);
        }
        #endif
    }
}