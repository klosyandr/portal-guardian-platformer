using UnityEngine;
using PortalGuardian.UI.Widgets;

namespace PortalGuardian.Component.Health
{
    public class HealthBarController : MonoBehaviour
    {
        private ProgressBarWidget _healthBar;
        private int _maxHealth;

        private void Awake()
        {
            _maxHealth = GetComponentInParent<HealthComponent>().MaxHealth;
            _healthBar = GetComponent<ProgressBarWidget>();
        }

        public void OnHealthChanged(int newValue)
        { 
            var value = (float) newValue / _maxHealth;
            _healthBar.SetProgress(value);
        }
    }

}