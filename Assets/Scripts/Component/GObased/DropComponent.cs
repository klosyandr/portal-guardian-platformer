using System;
using UnityEngine;

namespace PixelCrew.Component.GoBased{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private int _minTotalCount;        
        [SerializeField] private int _maxTotalCount;
        [SerializeField] private Drop[] _drops;
        [SerializeField] private Transform _target;

        System.Random random = new System.Random();

        public void Drop(){
            var count = random.Next(_minTotalCount, _maxTotalCount);

            float tempChance;
            for (int i = 0; i < count; i++){
                tempChance = (float)random.NextDouble();     
                foreach (var drop in _drops){
                    if (tempChance >= drop.LowChance && tempChance < drop.UpChance ) {
                        var newPos = GetRandomTarget();
                        var instansiate = Instantiate(drop.Prefab, _target.position + newPos, Quaternion.identity);
                        break;
                    }
                }
            }
        }

        private Vector3 GetRandomTarget(){
            return new Vector3(random.Next(-10, 10) * 0.01f, random.Next(-50, 50) * 0.01f, 0);
        }
    }

    
    [Serializable]
    public class Drop{
        [SerializeField] private string _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] [Range(0, 1)] private float _lowСhance;
        [SerializeField] [Range(0, 1)] private float _upСhance;


        public string Name => _name;
        public GameObject Prefab => _prefab;
        public float UpChance => _upСhance;
        public float LowChance => _lowСhance;
    } 
}
