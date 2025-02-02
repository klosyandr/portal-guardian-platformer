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
using PortalGuardian.Model.Definitions.Repositories.Items;
using PortalGuardian.Model.Definitions.Repositories;

namespace PortalGuardian.Creatures.Player
{
    public class Hero : Creature
    {
        [Header("Move")]
        [SerializeField] private MoveComponent _simpleMove;
        [SerializeField] private LadderMoveComponent _ladderMove;
        [SerializeField] private float _fallVelocity;
        [Header("Checked")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [Space] [Header("For Gizmos")]
        [SerializeField] private Vector3 _groundCheckPositionDelta;
        [SerializeField] private float _groundCheckRadius;
        
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private int _coinsToDispose = 5;
        [SerializeField] private SpawnComponent _throwSpawner;
        
        private Cooldown _speedUpCooldown = new Cooldown();
        
        private GameSession _session;
        
        private int CoinsCount => _session.Data.Inventory.Count(Keys.COIN);
        private int ChargeCount => _session.Data.Inventory.Count(Keys.CHARGE); 
        private InventoryData Inventory => _session.Data.Inventory;
        private string SelectedItemId =>_session.QuickInventory.SelectedItem.Id;

        protected override void Awake()
        {
            base.Awake();
            _tempMoveMode = _simpleMove;
            
            _simpleMove.OnJump += () => PlayEffects(Keys.JUMP);
            _simpleMove.OnMoved += PlayAnimationSimpleMove;

            _ladderMove.ChangeOnLadderStatus += ChangeOnLadderStatus;
            _ladderMove.OnMoved += PlayAnimationLadderMove;
        }

        public void Start()
        {
            _session = FindObjectOfType<GameSession>();
        
            var health = GetComponent<HealthComponent>();
            health.SetHealth(_session.Data.Hp.Value);
        }
        
        public override void SetDirection(Vector2 direction)
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
        
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _fallVelocity)
                {
                    PlayEffects(Keys.FALL);
                }
            }
        }
        
        protected override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }
        
        public void SpawnCoins()
        {
            var score = CoinsCount;
            var value = Mathf.Min(_coinsToDispose, score);
            Inventory.Remove(Keys.COIN, value);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = value;
            _hitParticles.emission.SetBurst(0, burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }
        
        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public void UseInventory()
        {
            if (IsSelectedItem(ItemTag.Potion))
            {
                UsePotion();
            }
        }
        
        private bool IsSelectedItem(ItemTag tag)
        {
            return _session.QuickInventory.SelectedDef.HasTag(tag);
        }
        
        private void UsePotion()
        {
            var potion = DefsFacade.I.Potions.Get(SelectedItemId);
            switch (potion.Effect)
            {
                case Effects.AddHP:
                    _session.Data.Hp.Value += (int) potion.Value;
                    break;
                case Effects.SpeedUp:
                    _speedUpCooldown.Value = _speedUpCooldown.TimeLasts + potion.Time;
                    _simpleMove.AdditionalSpeed = Mathf.Max(potion.Value, _simpleMove.AdditionalSpeed);
                    _speedUpCooldown.Reset();
                    break;
            }
            _session.Data.Inventory.Remove(potion.Id, 1);
        }
        
        public void Interact()
        {
            _interactionCheck.Check();
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

        public void NextThrow()
        {
            _session.ThrowInventory.SetNextItem();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = _simpleMove.IsGrounded ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadius);
        }
#endif

        public void DoThrowAttack()
        {
           if (!_throwCooldown.IsReady || !_session.Data.HasMelee) return;
           
           ThrowAndRemoveFromInventory();
        }    

        public void DoThrowAttackSeries()
        { 
            if (!_throwCooldown.IsReady || !_session.Data.HasMelee) return;
            
            StartCoroutine(ThrowAttackSeries());
        }        

        private void ThrowAndRemoveFromInventory()
        {
            _throwCooldown.Reset();

            var trowableId = _session.ThrowInventory.SelectedItem.Id;
            var trowableDef = DefsFacade.I.Trowable.Get(trowableId);

            _throwSpawner.SetPrefab(trowableDef.Projectile);
            PlayEffects("Melee");
            Inventory.Remove(trowableId, 1);
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
            if (!_session.Data.HasRange) return;

            PlayEffects("Range");
            _attackRange.Check();
        }
    }
}