using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrew.Creatures;

namespace PixelCrew{
    public class PlayerInputReader : MonoBehaviour{
        [SerializeField] private Player _player;

        public void OnMove(InputValue context){
            var direction = context.Get<Vector2>();
            _player.SetDirection(direction);
        }
        
        public void OnAirAttack(InputValue context){
            if (context.isPressed) _player.AirAttack();
            else _player.AirAttackSeries();             
        }

        public void OnFireAttack(InputValue context){
             _player.FireAttack();
        }
        
        public void OnInteract(InputValue context){
            _player.Interact();
        }
        
        public void OnHeal(InputValue context){
            _player.Heal();
        }
    }
}

