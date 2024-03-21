using System.Collections.Generic;
using PortalGuardian.Model;
using PortalGuardian.Utils.Disposables;
using UnityEngine;

namespace PortalGuardian.UI
{
    public class QuickInvenoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInvenory.Subscribe(Rebuild));
            Rebuild();  
        }

        private void Rebuild()
        {
            var inventory = _session.QuickInvenory.Inventory;
            
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
