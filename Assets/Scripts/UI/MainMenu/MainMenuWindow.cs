using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PortalGuardian.UI{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        public void OnShowSettings(){
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnStartGame(){
            _closeAction = () => { SceneManager.LoadScene("Level_Test"); };
            Close();
        }

        public void OnExit(){
            _closeAction = () =>
            {  Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif 
            };
            Close();          
        }
        
        public override void OnCloseAnimationComplete(){
            base.OnCloseAnimationComplete();
            
            _closeAction?.Invoke();
        }
    }
}