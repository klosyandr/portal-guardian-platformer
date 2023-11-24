using UnityEngine;


namespace PixelCrew.Component.Interactaction{
    public class DoInteractableComponent : MonoBehaviour
    {
        public void DoInteractable(GameObject go){
            var interactable = go.GetComponent<InteractableComponent>();
            if(interactable != null){
                interactable.Interact();
            }
        }
    }
}
