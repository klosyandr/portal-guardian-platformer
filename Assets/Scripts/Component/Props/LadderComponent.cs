using UnityEngine;

namespace PortalGuardian.Component.Props
{
    public class LadderComponent : MonoBehaviour
    {
        [SerializeField] private Transform _pointUp;    
        [SerializeField] private Transform _pointDown;

        public float Up => _pointUp.position.y;
        public float Down => _pointDown.position.y;
        public float CenterHorizontal => _pointDown.position.x;
    }
}