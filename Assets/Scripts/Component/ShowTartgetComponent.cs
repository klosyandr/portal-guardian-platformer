using UnityEngine;

namespace PortalGuardian.Component
{        
    public class ShowTartgetComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private CameraStateController _controller;
        [SerializeField] private float _delay = 0.5f;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_controller == null)
                _controller = FindObjectOfType<CameraStateController>();
        }
#endif

        public void ShowTarget()
        {
            _controller.SetPosition(_target.position);
            _controller.SetState(true);
            Invoke(nameof(MoveBack), _delay);
        }

        private void MoveBack()
        {
            _controller.SetState(false);
        }
    }
}
