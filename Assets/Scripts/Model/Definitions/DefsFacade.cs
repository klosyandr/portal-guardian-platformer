using PortalGuardian.Model.Definitions.Repositories;
using PortalGuardian.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PortalGuardian.Model.Definitions
{
    [CreateAssetMenu(menuName ="Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private ItemsRepository _items;
        [SerializeField] private TrowableRepository _trowableItems;
        [SerializeField] private PotionRepository _potions;
        [SerializeField] private PlayerDef _player;

        private static DefsFacade _instance;

        public ItemsRepository Items => _items; 
        public TrowableRepository Trowable => _trowableItems;
        public PotionRepository Potions => _potions;
        public PlayerDef Player => _player;
        
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}