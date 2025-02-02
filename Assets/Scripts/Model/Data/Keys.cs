using UnityEngine;

namespace PortalGuardian.Model.Data
{
    public struct AnimatorKeys
    {
        public static readonly int Y_VELOCITY = Animator.StringToHash("y-velocity");

        public static readonly int IS_GROUND = Animator.StringToHash("is-ground");
        public static readonly int RUN = Animator.StringToHash("is-running");
        public static readonly int CLIMB = Animator.StringToHash("is-climb");
        public static readonly int CLIMBING = Animator.StringToHash("is-climbing"); 
    }
    
    public struct EffectsKeys
    {
        public static readonly string JUMP = "Jump";
        public static readonly string FALL = "Fall";
    }
}

