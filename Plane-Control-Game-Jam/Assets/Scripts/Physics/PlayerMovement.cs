using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _speedWithLowestSpeedStat = 5f;
    [SerializeField]
    private float _speedBoostFractionOfBasePerSpeedStatLevel = .1f;
    [SerializeField]
    private float _accelerationTime = .1f;
    [SerializeField]
    private float _decelerationTime = .1f;
    [SerializeField]
    private float _rotationDegreesPerSec = 100;

    private void FixedUpdate()
    {
        int speedLevelups = PlayerStats.Instance.GetStat("speed").Value - 1;
        float maxSpeed = _speedWithLowestSpeedStat * (1 + _speedBoostFractionOfBasePerSpeedStatLevel * speedLevelups);

        Vector2 targetVelocity = maxSpeed * MovementInputDirection();
        Vector2 currentVelocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.z); // y is up
        Vector2 velocityChangeDirection = (targetVelocity - currentVelocity).normalized;

        float accel = CurrentAcceleration(currentVelocity, velocityChangeDirection, maxSpeed);

        Vector2 newVelocity = NewVelocity(maxSpeed, currentVelocity, accel
            , velocityChangeDirection, targetVelocity);

        _rigidbody.velocity = new Vector3(newVelocity.x, 0, newVelocity.y);

        if (_rigidbody.velocity.magnitude > 0 && targetVelocity.magnitude > 0)
        {
            float nextRotation = NextRotation(targetVelocity);
            //Debug.Log((nextRotation - _rigidbody.rotation.eulerAngles.y) / Time.fixedDeltaTime);
            _rigidbody.MoveRotation(Quaternion.Euler(0, nextRotation, 0));
        }
    }

    private float NextRotation(Vector2 targetMovementDirection)
    {
        // The rigidbody's velocity rotates gradually already, except it snaps rotation when you start moving
        // from 0 velocity, so gradually rotate towards the target direction.

        // Just aiming in the direction of WASD input feels bad because when you release while travelling
        // diagonally, you usually don't release both buttons in the same frame, so it ends up looking along
        // just 1 axis.

        Quaternion movementDirection = Quaternion.FromToRotation(Vector3.forward
            , new Vector3(targetMovementDirection.x, 0, targetMovementDirection.y));

        float movementDirectionAngle = movementDirection.eulerAngles.y;
        float currentDirectionAngle = _rigidbody.rotation.eulerAngles.y;

        float change = movementDirectionAngle - currentDirectionAngle;
        change = ((change + 540) % 360) - 180; // confine change between -180 and 180
        if (Mathf.Abs(change) > _rotationDegreesPerSec * Time.fixedDeltaTime)
        {
            change = Mathf.Sign(change) * _rotationDegreesPerSec * Time.fixedDeltaTime;
        }

        return currentDirectionAngle + change;
    }

    private Vector2 MovementInputDirection()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            direction += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            direction += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            direction += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;
        direction.x += Input.GetAxis("JoystickHorizontal");
        direction.y += Input.GetAxis("JoystickVertical");
        return direction.normalized;
    }

    private float CurrentAcceleration(Vector2 currentVelocity, Vector2 velocityChangeDirection, float maxSpeed)
    {
        float accelTime = Vector2.Dot(currentVelocity, velocityChangeDirection) > 0 ? _accelerationTime : _decelerationTime;
        if (accelTime == 0)
        {
            return float.PositiveInfinity;
        }
        return maxSpeed / accelTime;
    }

    private Vector2 NewVelocity(float maxSpeed, Vector2 currentVelocity, float accel
        , Vector2 velocityChangeDirection, Vector2 targetVelocity)
    {
        if (accel == float.PositiveInfinity)
        {
            return targetVelocity;
        }

        Vector2 newVelocity = currentVelocity + Time.fixedDeltaTime * accel * velocityChangeDirection;
        if (newVelocity.magnitude > maxSpeed)
        {
            newVelocity = newVelocity.normalized * maxSpeed;
        }

        Vector2 currentVelocityError = currentVelocity - targetVelocity;
        Vector2 newVelocityError = newVelocity - targetVelocity;
        if (Vector2.Dot(currentVelocityError, newVelocityError) < 0)
        {
            // Do this to prevent jittering around the target velocity.
            return targetVelocity;
        }

        return newVelocity;
    }
}
