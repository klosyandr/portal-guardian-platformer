using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Component.ColliderBase{
    public class ExitTriggerComponent : MonoBehaviour{
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private UnityEvent _action;

        private void OnTriggerExit2D(Collider2D other){            
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;

            _action?.Invoke();
        }
    }
}
