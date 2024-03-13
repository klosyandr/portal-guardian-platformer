using UnityEngine;

namespace PortalGuardian.Component.Interactable
{
    public class DoInteractableComponent : MonoBehaviour
    {
        public void DoInteractable(GameObject go)
        {
            var interactable = go.GetComponent<InteractableComponent>();
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
