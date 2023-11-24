using UnityEngine;

namespace PixelCrew.Creatures.Mobs{
    public class Frog : Creature
    {
        protected static readonly int isDeadKey = Animator.StringToHash("is-dead");
    
        protected override void FixedUpdate(){
            var xVelocity = _direction.x * _speed;
            _rigidbody.velocity = new Vector2(xVelocity,_rigidbody.velocity.y);

            UpdateSpriteDirection();
        }
    }
}

