using UnityEngine;

namespace PixelCrew.Component.Props{
    public class LadderComponent : MonoBehaviour
    {
        [SerializeField] private Transform _pointUp;    
        [SerializeField] private Transform _pointDown;

        public Transform Up => _pointUp;
        public Transform Down => _pointDown;
    }
}