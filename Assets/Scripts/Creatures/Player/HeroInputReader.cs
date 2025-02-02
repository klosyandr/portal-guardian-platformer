using UnityEngine;
using UnityEngine.InputSystem;

namespace PortalGuardian.Creatures.Player
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _player;

        public void OnMove(InputValue context)
        {
            var direction = context.Get<Vector2>();
            _player.SetDirection(direction);
        }
    }
}

