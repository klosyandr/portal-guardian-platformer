using PortalGuardian.Model;
using PortalGuardian.Model.Data;
using PortalGuardian.Model.Definitions;
using PortalGuardian.Model.Definitions.Repositories.Items;
using PortalGuardian.Utils.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PortalGuardian.UI
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selected;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private ItemTag _tag;
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private int _index;

        public void Start()
        {
            var session = FindObjectOfType<GameSession>();
            var model = session.GetInventory(_tag);
            model.SelectedIndex.SubscribeAndInvoke(OnIndexChanged);
        }

        private void OnIndexChanged(int newValue, int _)
        {
            _selected.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
            var def = DefsFacade.I.Items.Get(item.Id);
            _icon.sprite = def.Icon;
            _value.text = def.HasTag(_tag) ? $"x{item.Value.ToString()}" : string.Empty;
        }

    }
}