using System;
using PortalGuardian.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PortalGuardian.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Trowable", fileName = "Trowable")]
    public class TrowableRepository : DefRepository<TrowableDef>
    {
    }

    [Serializable]
    public struct TrowableDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
} 