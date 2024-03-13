using System.Linq;
using PortalGuardian.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PortalGuardian.Component.ColliderBase
{

    public class CheckCircleOverlap : MonoBehaviour
    {
        
        [SerializeField] private float _radius = 1f;
        [SerializeField] private UnityEvent<GameObject> _OnOverlapEvent;
        [SerializeField] private LayerMask  _mask;
        [SerializeField] private string[] _tags; 
        
        private readonly Collider2D[] _interactionResult = new Collider2D[10];
        
        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _interactionResult, _mask);

            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResult[i];
                var isInTags =_tags.Any(tag => overlapResult.CompareTag(tag));
                if (isInTags)
                {
                    _OnOverlapEvent?.Invoke(_interactionResult[i].gameObject); 
                }
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position,Vector3.forward, _radius);
        }
        #endif
    }
}