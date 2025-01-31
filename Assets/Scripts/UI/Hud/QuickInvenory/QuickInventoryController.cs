using System.Collections.Generic;
using PortalGuardian.Model;
using PortalGuardian.Model.Data;
using PortalGuardian.Model.Definitions.Repositories.Items;
using PortalGuardian.Utils.Disposables;
using UnityEngine;

namespace PortalGuardian.UI
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;
        [SerializeField] private ItemTag _tag;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private QuickInventoryModel _model;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            _model = session.GetInventory(_tag);
            _trash.Retain(_model.Subscribe(Rebuild));
            Rebuild();  
        }

        private void Rebuild()
        {
            var inventory = _model.Inventory;
            
            //create required items
            for(var i = _createdItem.Count; i < inventory.Length; i++)
            {
                var item = Instantiate(_prefab, _container);
                _createdItem.Add(item);
            }

            //update data and activate
            for(var i = 0; i < inventory.Length; i++)
            {
                _createdItem[i].SetData(inventory[i], i);
                _createdItem[i].gameObject.SetActive(true);
            }

            // hide unused items
            for(var i = inventory.Length; i < _createdItem.Count; i++)
            {
                _createdItem[i].gameObject.SetActive(false);
            }

        }
    }
}
