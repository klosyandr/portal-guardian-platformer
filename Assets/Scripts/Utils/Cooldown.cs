using System;
using UnityEngine;

namespace PortalGuardian.Utils{

    [Serializable]
    public class Cooldown{
        [SerializeField] private float _value;

        private float _timeUp;

        public bool IsReady => _timeUp <= Time.time;
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public float TimeLasts => Mathf.Max(_timeUp - Time.deltaTime, 0);

        public void Reset(){
            _timeUp = Time.time + _value;
        }

    }
}