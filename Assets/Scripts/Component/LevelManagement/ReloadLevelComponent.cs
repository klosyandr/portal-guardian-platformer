using UnityEngine;
using UnityEngine.SceneManagement;

namespace PortalGuardian.Component.LevelManagement
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {           
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}