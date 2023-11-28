using PortalGuardian.Component.GoBased;
using PortalGuardian.Utils;
using UnityEngine;

namespace PortalGuardian.Creatures.Mobs{
    public class PartShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private SpawnComponent _attack;              
        [SerializeField] private Cooldown _cooldown;
        public Cooldown Cooldown => _cooldown; 

        private Animator _animator;
        private static readonly int _attackKey = Animator.StringToHash("attack");

        private void Awake(){
            _animator = GetComponent<Animator>();
        }

        private void OnAttack()
        {
            _attack.Spawn();
        }

        public void Attack()
        {
            _animator.SetTrigger(_attackKey);
        }
    }
}

