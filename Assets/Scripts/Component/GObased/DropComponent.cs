using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PortalGuardian.Component.GoBased
{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private int _minTotalCount;        
        [SerializeField] private int _maxTotalCount;
        [SerializeField] private DropData[] _drops;
        [SerializeField] private DropEvent _onDropCalculated;
        [SerializeField] private bool _spawnOnEnable;

        private void OnEnable()
        {
            if (_spawnOnEnable)
            {
                CalculateDrop();
            }
        }

        [ContextMenu("CalculateDrop")]
        public void CalculateDrop()
        {
            var count = Random.Range(_minTotalCount, _maxTotalCount);
            var itemToDrop = new GameObject[count];
            var itemCount = 0;
            var total = _drops.Sum(dropData => dropData.Probability);
            var sortedDrop = _drops.OrderBy(dropData => dropData.Probability);

            while (itemCount < count)
            {
                var current = 0f;
                var random = Random.value * total;
                
                foreach (var dropData in sortedDrop)
                {
                    current += dropData.Probability;
                    if (current >= random)
                    {
                        itemToDrop[itemCount] = dropData.Drop;
                        itemCount++;
                        break;
                    }
                }
            }

            _onDropCalculated?.Invoke(itemToDrop);
        }
        
        [Serializable]
        public class DropEvent : UnityEvent<GameObject[]>
        {}

        [Serializable]
        public class DropData
        {
            public GameObject Drop;
            [Range(0f, 100f)] public float Probability;
        } 
    }    
}
