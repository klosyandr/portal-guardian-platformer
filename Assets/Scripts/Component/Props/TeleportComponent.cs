using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PortalGuardian.Component.Props
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;

        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var input = target.GetComponent<PlayerInput>();
            SetLockInput(input, true);

            var sprite = target.GetComponent<SpriteRenderer>();
            yield return SetAlpha(sprite, 0);
            target.SetActive(false);     

            yield return SetPosition(target);

            target.SetActive(true);        
            yield return SetAlpha(sprite, 1);
            
            SetLockInput(input, false);            
        }

        private void SetLockInput(PlayerInput input, bool isLocked)
        {
            if (input != null)
            {
                input.enabled = !isLocked;
            }
        }

        private IEnumerator SetAlpha(SpriteRenderer sprite, float destAlpha)
        {
            var time = 0f;
            var spriteAlpha = sprite.color.a;
            while (time < _alphaTime)
            {
                time += Time.deltaTime;
                var progress = time / _alphaTime;
                var tempAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);
                var color = sprite.color;
                color.a = tempAlpha;
                sprite.color = color;

                yield return null;
            }
        }
        
        private IEnumerator SetPosition(GameObject target)
        {
            var time = 0f;
            while(time < _moveTime)
            {
                time += Time.deltaTime;
                var progress = time / _moveTime;
                target.transform.position = Vector3.Lerp(target.transform.position, _destTransform.position, progress);

                yield return null;
            }
        }

    }
}
