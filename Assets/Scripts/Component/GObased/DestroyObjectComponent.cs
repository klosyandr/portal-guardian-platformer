using UnityEngine;

namespace PortalGuardian.Component.GoBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestoy;

        public void DestroyObject(){
            Destroy(_objectToDestoy);
        }
    }
}