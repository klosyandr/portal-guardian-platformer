using System;
using UnityEngine;

namespace PortalGuardian.Creatures
{
    public abstract class IMoveComponent : MonoBehaviour
    {   
        [Header("Params")]
        [SerializeField] protected float _speed;

        protected Rigidbody2D _rigidbody;        
        protected Vector2 _direction;
        protected float _additionalSpeed;
        
        public Action OnMoved;

        public Vector2 Direction 
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public float AdditionalSpeed 
        {
            set { _additionalSpeed = value; }
            get { return _additionalSpeed; }
        }

        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();       
        }

        public abstract void Move();
    }
}