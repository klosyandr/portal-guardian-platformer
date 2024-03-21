using UnityEngine;
using UnityEngine.InputSystem;

namespace PortalGuardian.Creatures.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public void OnMove(InputValue context){
            var direction = context.Get<Vector2>();
            _player.SetDirection(direction);
        }
        
        public void OnAirAttack(InputValue context){
            if (context.isPressed) _player.DoThrowAttack();
            else _player.DoThrowAttackSeries();             
        }

        public void OnFireAttack(InputValue context){
             _player.RangeAttack();
        }
        
        public void OnInteract(InputValue context){
            _player.Interact();
        }
        
        public void OnHeal(InputValue context){
            _player.Heal();
        }

        public void OnNextItem(InputValue context){
            _player.NextItem();
        }
    }
}

