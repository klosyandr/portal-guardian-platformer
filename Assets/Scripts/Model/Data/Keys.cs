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
        public static readonly int HIT = Animator.StringToHash("hit");
        public static readonly int ATTACK = Animator.StringToHash("attack");
        public static readonly int IS_DEAD = Animator.StringToHash("is-dead");
    
    }
    
    public struct Keys
    {
        public static readonly string JUMP = "Jump";
        public static readonly string FALL = "Fall";
        public static readonly string RANGE = "Range";
        public static readonly string COIN = "Coin";
        public static readonly string CHARGE = "Charge";
    }
}

