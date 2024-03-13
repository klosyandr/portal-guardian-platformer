using UnityEngine;

namespace PortalGuardian.UI{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int _showKey = Animator.StringToHash("show");
        private static readonly int _hideKey = Animator.StringToHash("hide");

        protected virtual void Start(){
            _animator = GetComponent<Animator>();
            _animator.SetTrigger(_showKey);
        }

        public void Close(){
            _animator.SetTrigger(_hideKey);
        }

        public virtual void OnCloseAnimationComplete(){
            Destroy(gameObject);
        }
    }
}