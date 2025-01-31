using System;
using System.Collections.Generic;
using System.Linq;
using PortalGuardian.Model.Definitions;
using PortalGuardian.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PortalGuardian.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChanged(string id, int value);
        public OnInventoryChanged OnChanged;

        public void Add(string id, int value)
        {
            if (value <= 0) return;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);
            if (item == null || !itemDef.HasTag(ItemTag.Stackable))
            {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            item.Value += value;

            OnChanged?.Invoke(id, Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;
            
            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;
            if(item.Value <= 0)
            {
                _inventory.Remove(item);
            }            
            OnChanged?.Invoke(id, Count(id));
        }


        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id)
                {
                    return itemData;
                }
            }
            return null;
        }

        public int Count(string id)
        {
            foreach (var item in _inventory)
            {
                if (item.Id == id)
                {
                    return item.Value;
                }
            }
            return 0;
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            var retValue = new List<InventoryItemData>();
            foreach(var item in _inventory)
            {
                var itemDef = DefsFacade.I.Items.Get(item.Id);
                var isAllRequirementsMet = tags.All(x => itemDef.HasTag(x));
                if (isAllRequirementsMet)
                {
                    retValue.Add(item);
                }
            }
            return retValue.ToArray();
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string Id;
        public int Value;

        public InventoryItemData(string id){
            Id = id;
        }

    }
}