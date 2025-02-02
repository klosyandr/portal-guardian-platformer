using System;
using PortalGuardian.Component.ColliderBase;
using UnityEngine;

namespace PortalGuardian.Creatures
{
    public class MoveComponent : IMoveComponent
    {
        [Header("Params")]
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private bool _hasHorizontal;
        [SerializeField] private bool _hasVertical;
        [SerializeField] private bool _hasDoubleJump;

        [Header("Checked")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;

        private bool _isGrounded;
        private bool _allowDoubleJump;

        public Action OnJump;
        
        public bool IsGrounded => _isGrounded;

        private void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;
        }

        public override void Move()
        {
            var xVelocity = _hasHorizontal ? CalculateXVelocity() : _rigidbody.velocity.x;
            var yVelocity = _hasVertical ? CalculateYVelocity() : _rigidbody.velocity.y;
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            UpdateSpriteDirection();
            OnMoved?.Invoke();
        }

        private float CalculateSpeed()
        {
            return _speed + _additionalSpeed;
        }

        private float CalculateXVelocity()
        {
            return _direction.x * CalculateSpeed();
        }

        private float CalculateYVelocity()
        {
            if (_hasDoubleJump && _isGrounded) 
            {
                _allowDoubleJump = true;
            }

            var yVelocity = _rigidbody.velocity.y;
            var isJump = _direction.y > 0;

            if (isJump)
            {
                var isFalling = _rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            if(_hasDoubleJump && !_isGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;
                _direction.y = 0; 
                OnJump?.Invoke();
                return _jumpSpeed;
            }

            if(_isGrounded)
            {
                yVelocity = _jumpSpeed;
                _direction.y = 0;
                OnJump?.Invoke();
            }
            return yVelocity; 
        }
        
        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if(_direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

}

