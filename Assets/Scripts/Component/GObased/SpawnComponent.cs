using UnityEngine;

namespace PortalGuardian.Component.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]

        public void Spawn()
        {
            var instansiate = Instantiate(_prefab, _target.position, Quaternion.identity);
            instansiate.transform.localScale = _target.lossyScale;
        }
    }
}
