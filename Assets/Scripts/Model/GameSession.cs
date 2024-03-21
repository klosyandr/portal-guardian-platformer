using PortalGuardian.Model.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PortalGuardian.Model
{

    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;        
        
        private PlayerData _save;

        public PlayerData Data => _data;   
        public QuickInvenoryModel QuickInvenory { get; private set; }     

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
            QuickInvenory = new QuickInvenoryModel(_data);
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
    }
}