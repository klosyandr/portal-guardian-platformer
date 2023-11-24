using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Component.Health{

    public class HealthComponent : MonoBehaviour{
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _OnHeal;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private UnityEvent<int> _onChange; 

        public void ModifyHealth(int hpDelta){
            _health += hpDelta;
            _onChange?.Invoke(_health);
            Debug.Log($"HPmodify: {hpDelta}; health: {_health}");
            if (hpDelta < 0) _onDamage?.Invoke();
            if (hpDelta > 0) _OnHeal?.Invoke();
            if (_health <= 0) _onDie?.Invoke();         
        }

        public void SetHealth(int health){
            _health = health;
        }

        #if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth(){
            _onChange?.Invoke(_health);
        }
        #endif
    }
}
