using System;
using PortalGuardian.Model.Data.Properties;
using UnityEngine;

namespace PortalGuardian.Model.Data
{

    [Serializable]
    public class PlayerData{
        [SerializeField] InventoryData _inventory;
        public IntProperty Hp = new IntProperty();
        public bool _hasRange;
        public bool _hasMelee;

        public InventoryData Inventory => _inventory;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}