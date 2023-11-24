using UnityEngine;

namespace PixelCrew.Component.Props{
    public class TeleportComponent : MonoBehaviour
        {
            [SerializeField] private Transform _destTransform;

            public void Teleport(GameObject target){
                target.transform.position = _destTransform.position;
            }
        }
    }
