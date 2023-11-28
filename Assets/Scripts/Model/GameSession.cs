using PortalGuardian.Model.Data;
using UnityEngine;

namespace PortalGuardian.Model{

    public class GameSession : MonoBehaviour{
        [SerializeField] private PlayerData _dataTemp;        
        [SerializeField] private PlayerData _dataStartLevel;

        public PlayerData Data => _dataTemp;        

        private void Awake(){
            if (IsSessionExist()){               
                DestroyImmediate(gameObject);
            }
            else {                            
                DontDestroyOnLoad(this);
                this.SaveStartData();
            }
        }

        private bool IsSessionExist(){
            var sessions = FindObjectsOfType<GameSession>(); 
            foreach (var gameSession in sessions){ 
                if (gameSession != this){
                    return true; 
                }
            }
            return false;
        }

        public void LoadStartData(){   
        
            _dataTemp = _dataStartLevel.Clone();   

        }

        public void SaveStartData(){  
            _dataStartLevel = _dataTemp.Clone();   
       
        }
    }
}