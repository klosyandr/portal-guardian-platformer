using System;
using PortalGuardian.Model.Data.Properties;
using PortalGuardian.Model.Definitions;
using PortalGuardian.Model.Definitions.Repositories.Items;
using PortalGuardian.Utils.Disposables;
using UnityEngine;

namespace PortalGuardian.Model.Data
{
    public class QuickInventoryModel
    {
        private readonly PlayerData _data;
        private readonly ItemTag _tag;

        public InventoryItemData[] Inventory { get; private set; }
        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged;

        public InventoryItemData SelectedItem
        {
            get
            {
                if(Inventory.Length > 0 && Inventory.Length >= SelectedIndex.Value)
                    return Inventory[SelectedIndex.Value];
                return null;
            }
        }

        public ItemDef SelectedDef => DefsFacade.I.Items.Get(SelectedItem?.Id);

        public QuickInventoryModel(PlayerData data, ItemTag tag)
        { 
            _data = data;
            _tag = tag;
            Inventory = _data.Inventory.GetAll(tag);
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        private void OnChangedInventory(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, x => x.Id == id);
            if (indexFound != -1)
            {                 
                Inventory = _data.Inventory.GetAll(_tag);
                SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
                OnChanged?.Invoke();
            }
        } 

        public void SetNextItem()
        {
            SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public IDisposable Subscribe(Action call)
        {    
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }
    }
}