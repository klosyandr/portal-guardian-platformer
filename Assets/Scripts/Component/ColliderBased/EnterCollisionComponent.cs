using PortalGuardian.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace PortalGuardian.Component.ColliderBase
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private UnityEvent<GameObject> _action;

        private void OnCollisionEnter2D(Collision2D other){
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;

            _action?.Invoke(other.gameObject);
        }
    }
}