using PortalGuardian.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PortalGuardian.Component.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit(){
            var session = FindObjectOfType<GameSession>();            
            session.SaveStartData();
            SceneManager.LoadScene(_sceneName);
        }
     }
}
