using PixelCrew.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Component.LevelManagement{
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
