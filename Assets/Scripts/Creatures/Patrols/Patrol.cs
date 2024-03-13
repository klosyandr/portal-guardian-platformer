using System.Collections;
using UnityEngine;

namespace PortalGuardian.Creatures.Patrols
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
