using System;
using UnityEngine;

namespace PixelCrew.Model.Data{

    [Serializable]
    public class PlayerData{
        [SerializeField] InventoryData _inventory;
        public int HP;
        public bool _hasAttackFire;
        public bool _hasAttackAir;

        public InventoryData Inventory => _inventory;

        public PlayerData Clone(){
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}