using System;
using UnityEngine;
using System.Linq;

namespace PortalGuardian.Component.GoBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
           var spawner = _spawners.FirstOrDefault(element => element.Id == id);
           spawner?.Component.Spawn();
        }

    }

    [Serializable]
    public class SpawnData{
        public string Id;
        public SpawnComponent Component;
    }
}