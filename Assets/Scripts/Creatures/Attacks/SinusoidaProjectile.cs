using UnityEngine;

namespace PortalGuardian.Creatures.Attacks
{
    public class SinusoidaProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;

        private float _posY;
        private float _time;

        protected override void Start()
        {
            base.Start();
            _posY = _rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;  
            position.x += _direction * _speed;
            position.y = _posY + Mathf.Sin(_time * _frequency) * _amplitude;
            _rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }

    }
}
