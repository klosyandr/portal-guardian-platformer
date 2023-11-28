using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PortalGuardian.Component.GoBased{
    public class DropComponent : MonoBehaviour
    {
        [SerializeField] private int _minTotalCount;        
        [SerializeField] private int _maxTotalCount;
        [SerializeField] private Drop[] _drops;
        [SerializeField] private Transform _target;


        public void Drop(){
            var count = Random.Range(_minTotalCount, _maxTotalCount);

            float tempChance;
            for (int i = 0; i < count; i++){
                tempChance = Random.Range(0f,1f);     

                foreach (var drop in _drops){
                    if (tempChance >= drop.LowChance && tempChance < drop.UpChance ) {
                        var instansiate = Instantiate(drop.Prefab, _target.position, Quaternion.identity);
                        var rb = instansiate.GetComponent<Rigidbody2D>();
                        rb.AddForce(new Vector2(Random.Range(-1f, 1f) * 15, Random.Range(1, 3) * 15), ForceMode2D.Impulse);    
                        break;
                    }
                }
            }
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
