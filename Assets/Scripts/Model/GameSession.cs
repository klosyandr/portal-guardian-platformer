using Mono.Cecil;
using PortalGuardian.Model.Data;
using PortalGuardian.Model.Definitions.Repositories.Items;
using PortalGuardian.Utils.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PortalGuardian.Model
{

    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;        
        
        private PlayerData _save;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }
        public QuickInventoryModel ThrowInventory { get; private set; }

        private void Awake()
        {
            if (IsSessionExist())
            {               
                DestroyImmediate(gameObject);
            }
            else
            {                            
                DontDestroyOnLoad(this);
                InitModels();
                SaveStartData();
                LoadHud();
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data, ItemTag.Usable);
            ThrowInventory = new QuickInventoryModel(_data, ItemTag.Throwable);
            _trash.Retain(QuickInventory);
            _trash.Retain(ThrowInventory);
        }

        public QuickInventoryModel GetInventory(ItemTag tag)
        {
            switch (tag)
            {
                case ItemTag.Usable:
                    return QuickInventory;
                case ItemTag.Throwable:
                    return ThrowInventory;
                default:
                    return null;
            }
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>(); 
            foreach (var gameSession in sessions)
            { 
                if (gameSession != this)
                {
                    return true; 
                }
            }
            return false;
        }

        public void LoadStartData()
        {   
            _data = _save.Clone();   
        }

        public void SaveStartData()
        {  
            _save = _data.Clone();   
        }

        private void Oestroy()
        {
            _trash.Dispose();
        }
    }
}