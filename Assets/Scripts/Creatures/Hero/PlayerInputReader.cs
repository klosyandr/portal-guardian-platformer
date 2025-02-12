using UnityEngine;
using UnityEngine.InputSystem;

namespace PortalGuardian.Creatures.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _player;

        public void OnMove(InputValue context)
        {
            var direction = context.Get<Vector2>();
            _player.SetDirection(direction);
        }
        
        public void OnThrow(InputValue context)
        {
            if (context.isPressed) _player.DoThrowAttack();
            else _player.DoThrowAttackSeries();             
        }

        public void OnAttack(InputValue context)
        {
             _player.RangeAttack();
        }
        
        public void OnInteract(InputValue context)
        {
            _player.Interact();
        }
        
        public void OnUseInventory(InputValue context)
        {
            _player.UseInventory();
        }

        public void OnNextItem(InputValue context)
        {
            Debug.Log("OnNextItem");
            _player.NextItem();
        }

        public void OnNextThrow(InputValue context)
        {
            _player.NextThrow();
        }
    }
}

