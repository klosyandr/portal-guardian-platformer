using System.Collections;
using PortalGuardian.Component.ColliderBase;
using PortalGuardian.Component.GoBased;
using PortalGuardian.Creatures.Patrols;
using UnityEngine;

namespace PortalGuardian.Creatures.Mobs
{
    
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCoolDown = 1f;
        [SerializeField] private float _missCoolDown = 1f;

        private Coroutine _current;
        private GameObject _target;
        private SpawnListComponent _particles;
        private Creature _creature;
        private bool _isDead;
        private Animator _animator;
        private Patrol _patrol;
        private static readonly int _isDeadKey = Animator.StringToHash("is-dead");

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        private void StartState(IEnumerator coroutine)
        {            
            _creature.SetDirection(Vector2.zero);

            if (_current != null){
                StopCoroutine(_current);
            }
            _current = StartCoroutine(coroutine);
        }


        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;

            _target = go;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero ()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();

            if(direction.x > 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }
         
        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer && _groundCheck.IsTouchingLayer)
            {
                if(_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missCoolDown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }
            StartState(GoToHero());
        }

        
        public void OnDie()
        {
            _isDead = true;            
            _animator.SetBool(_isDeadKey, true);
            _creature.SetDirection(Vector2.zero);

            if (_current != null)
            {
                StopCoroutine(_current);
            }
        }
    }
}