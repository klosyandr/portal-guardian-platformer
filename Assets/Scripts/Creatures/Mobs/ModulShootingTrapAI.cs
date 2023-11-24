using System.Collections.Generic;
using PixelCrew.Component.ColliderBase;
using PixelCrew.Component.GoBased;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs{
    public class ModulShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private List<PartShootingTrapAI> _heads;
        private int _tempHead = -1;
        private DestroyObjectComponent _destroy;

        private void Awake(){
            _destroy = GetComponent<DestroyObjectComponent>();
        }

        private void Update(){
            if (_vision.IsTouchingLayer){
                if (_cooldown.IsReady) {
                    _cooldown.Reset();
                    _tempHead = _tempHead < _heads.Count - 1 ? _tempHead + 1 : 0;
                    var temp = _heads[_tempHead];
                    temp.Attack();
                    
                }
            }
        }

        public void DestroyPart(PartShootingTrapAI head){
            _heads.Remove(head);
            if (_heads.Count == 0){
                _destroy.DestroyObject();
            }
        }
    }

}

