using PortalGuardian.Component.ColliderBase;
using PortalGuardian.Component.GoBased;
using PortalGuardian.Utils;
using UnityEngine;

namespace PortalGuardian.Creatures.Mobs{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;

        [Header("Melee")]
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        [SerializeField] private Cooldown _meleeCooldown;  
        
        [Header("Range")]
        [SerializeField] private SpawnComponent _rangeAttack;              
        [SerializeField] private Cooldown _rangeCooldown;

        private Animator _animator;
        private static readonly int _meleeKey = Animator.StringToHash("melee");
        private static readonly int _rangeKey = Animator.StringToHash("range");

        private void Awake(){
            _animator = GetComponent<Animator>();
        }

        private void Update(){
            if (_vision.IsTouchingLayer){
                if (_meleeCanAttack.IsTouchingLayer){
                    if (_meleeCooldown.IsReady){
                        _meleeCooldown.Reset();
                        MeleeAttack();
                        return;
                    }
                }

                if (_rangeCooldown.IsReady){
                    _rangeCooldown.Reset();
                    RangeAttack();
                }
            }

        }

        private void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

        private void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        private void RangeAttack()
        {
            _animator.SetTrigger(_rangeKey);
        }

        private void MeleeAttack()
        {
            _animator.SetTrigger(_meleeKey);
        }


    }
}

