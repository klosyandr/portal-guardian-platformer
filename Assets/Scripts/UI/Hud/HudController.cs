using PortalGuardian.Model;
using PortalGuardian.Model.Definitions;
using PortalGuardian.UI.Widgets;
using UnityEngine;

namespace PortalGuardian.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Hp.OnChanged += OnHealthChanged;
            OnHealthChanged(_session.Data.Hp.Value, _session.Data.Hp.Value);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {            
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float) newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        private void OnDestroy(){
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}

