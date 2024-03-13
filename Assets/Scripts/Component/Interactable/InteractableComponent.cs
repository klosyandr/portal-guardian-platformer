using UnityEngine;
using UnityEngine.Events;

namespace PortalGuardian.Component.Interactable
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Interact()
        {
            _action?.Invoke();
        }
    }
}
