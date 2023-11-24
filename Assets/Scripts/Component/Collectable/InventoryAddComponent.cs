using UnityEngine;
using PixelCrew.Creatures;
using PixelCrew.Model.Definitions;

namespace PixelCrew.Component.Health{
    public class InventoryAddComponent : MonoBehaviour{
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _value = 1;

        public void Modify(GameObject go){    
            var player = go.GetComponent<Player>();
            player.AddInInventory(_id, _value);
        }
    }
}
