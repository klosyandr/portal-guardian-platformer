using UnityEngine;

namespace PortalGuardian.Component.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void ApplyModify(GameObject target){ 
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }
        }
    }
}
