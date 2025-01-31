using System;
using PortalGuardian.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PortalGuardian.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Potions", fileName = "Potions")]
    public class PotionRepository : DefRepository<PotionDef>
    {
    }

    [Serializable]
    public struct PotionDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private Effects _effect;
        [SerializeField] private float _value;
        [SerializeField] private float _time;

        public string Id => _id;
        public Effects Effect => _effect;
        public float Value => _value;
        public float Time => _time;
    }

    public enum Effects
    {
        AddHP,
        SpeedUp
    }
} 