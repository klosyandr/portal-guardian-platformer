using UnityEngine;
using UnityEngine.Events;

namespace PortalGuardian.Component.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _OnHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent<int> _onChange;

        public int MaxHealth {get => _maxHealth;}

        public void ModifyHealth(int hpDelta)
        {
            _health += hpDelta;
            _onChange?.Invoke(_health);
            if (hpDelta < 0) _onDamage?.Invoke();
            if (hpDelta > 0) _OnHeal?.Invoke();
            if (_health <= 0) _onDie?.Invoke();         
        }

        public void SetHealth(int health)
        {
            _health = health;
        }

        #if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange?.Invoke(_health);
        }
        #endif
    }
}
