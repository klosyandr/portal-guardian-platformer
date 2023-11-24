using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemsDef : ScriptableObject{
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id){
            foreach (var ItemDef in _items){
                if (ItemDef.Id == id){
                    return ItemDef;
                }
            }
            return default;
        } 

#if UNITY_EDITOR
    public ItemDef[] ItemsForEditor => _items;
#endif
    }

    [Serializable]
    public struct ItemDef{
        [SerializeField] private string _id;
        public string Id => _id;
        [SerializeField] private bool _isStack;
        public bool IsStack => _isStack;
        
        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
} 