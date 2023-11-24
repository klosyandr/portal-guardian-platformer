using PixelCrew.Component.GoBased;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs{
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

