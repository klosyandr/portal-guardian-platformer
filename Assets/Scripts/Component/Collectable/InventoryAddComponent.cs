using UnityEngine;
using PortalGuardian.Creatures.Player;
using PortalGuardian.Model.Definitions.Repositories.Items;

namespace PortalGuardian.Component.Collectable
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _value = 1;

        public void Modify(GameObject go)
        {    
            var hero = go.GetComponent<Hero>();
            hero.AddInInventory(_id, _value);
        }
    }
}
