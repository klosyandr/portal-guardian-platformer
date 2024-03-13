using UnityEngine;
using PortalGuardian.Creatures.Player;
using PortalGuardian.Model.Definitions;

namespace PortalGuardian.Component.Collectable
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _value = 1;

        public void Modify(GameObject go)
        {    
            var player = go.GetComponent<Player>();
            player.AddInInventory(_id, _value);
        }
    }
}
