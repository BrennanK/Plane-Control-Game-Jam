using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It follows an elliptical path which overlaps the player.
// Should spawn and despawn slightly away from the player.

public class ProjectileMovesLikeBoomerang : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private bool _aimAtEnemyNotForwards;
    [SerializeField] private float _directionOffsetAngleDegrees;
    [SerializeField] private float _speed;
    [SerializeField] private float _ellipseLength;
    [SerializeField] private float _ellipseWidth;
    [SerializeField] private bool _logExistenceDuration;

    private Rigidbody _playerRigidbody;
    private float _a; // semimajor axis
    private float _b; // semiminor axis
    private float _aa;
    private float _bb;
    private float _ellipseRotation;
    private Vector2 _ellipseCenter;
    private Vector2 _unrotatedPositionRelativeEllipseCenter;
    private bool _unrotatedEllipseHasBeenInPositiveY;
    private float _existenceStartTime;

    public void Initialize(Rigidbody playerRigidbody, Transform target)
    {
        _existenceStartTime = Time.time;
        _playerRigidbody = playerRigidbody;
        
        _a = _ellipseLength / 2;
        _b = _ellipseWidth / 2;
        _aa = _a * _a;
        _bb = _b * _b;

        Vector3 direction;
        if (_aimAtEnemyNotForwards)
            direction = ProjectileMovementProcessor.DirectionToTargetInPlane(_playerRigidbody.position, target);
        else
            direction = ProjectileMovementProcessor.DirectionInFront(_playerRigidbody.transform);
        _ellipseRotation = Quaternion.FromToRotation(Vector3.forward, direction).eulerAngles.y * Mathf.Deg2Rad
            + _directionOffsetAngleDegrees * Mathf.Deg2Rad;

        _ellipseCenter = new Vector2(0, _a);
        _unrotatedPositionRelativeEllipseCenter = -_ellipseCenter;

        _rigidbody.position = _playerRigidbody.position;
        _rigidbody.transform.position = _rigidbody.position;

        // initial rotation in case the wrong rotation could be visible for 1 frame
        UpdateVelocity();
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, _rigidbody.velocity);
        _rigidbody.rotation = rotation;
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
        _rigidbody.MoveRotation(Quaternion.FromToRotation(Vector3.forward, _rigidbody.velocity));
        CheckDestroyBasedOnPositionInUnrotatedEllipse();
    }

    private void CheckDestroyBasedOnPositionInUnrotatedEllipse()
    {
        float x = _unrotatedPositionRelativeEllipseCenter.x;
        float y = _unrotatedPositionRelativeEllipseCenter.y;
        if (y > 0)
            _unrotatedEllipseHasBeenInPositiveY = true;
        else if (_unrotatedEllipseHasBeenInPositiveY && x > 0)
        {
            if (_logExistenceDuration)
                Debug.Log("boomerang existence duration: " + (Time.time - _existenceStartTime));
            Destroy(gameObject);
        }
    }

    private void UpdateVelocity()
    {
        Vector2 velocityWithoutPlayerMovement = _speed * DirectionAlongUnrotatedEllipse(_unrotatedPositionRelativeEllipseCenter);

        _unrotatedPositionRelativeEllipseCenter += velocityWithoutPlayerMovement * Time.fixedDeltaTime;

        Vector2 positionRelativePlayer = _unrotatedPositionRelativeEllipseCenter + _ellipseCenter;
        positionRelativePlayer = ProjectileMovementProcessor.RotateVector(positionRelativePlayer, -_ellipseRotation);

        Vector3 currentPositionRelativePlayer = _rigidbody.position - _playerRigidbody.position;
        Vector2 currentPositionRelativePlayer2D = new Vector2(currentPositionRelativePlayer.x, currentPositionRelativePlayer.z);
        Vector2 positionError = positionRelativePlayer - currentPositionRelativePlayer2D;

        Vector2 velocity = positionError / Time.fixedDeltaTime;
        _rigidbody.velocity = new Vector3(velocity.x, 0, velocity.y) + _playerRigidbody.velocity;
    }

    private Vector2 DirectionAlongUnrotatedEllipse(Vector2 positionRelativeUnrotatedEllipseCenter)
    {
        // formula of an ellipse with semimajor and semiminor axes: x^2/a^2 + y^2/b^2 = 1
        // use implicit differentiation to get y_prime = 2x/(aa) + 2y * y_prime / (bb) = 0

        float x = positionRelativeUnrotatedEllipseCenter.x;
        float y = positionRelativeUnrotatedEllipseCenter.y;
        float slope = -x / _bb * _aa / y;
        Vector2 result = new Vector2(1, slope);
        result.Normalize();

        if (float.IsNaN(slope) || !float.IsFinite(slope))
        {
            // y is 0
            if (x > 0)
                return new Vector2(0, 1);
            else
                return new Vector2(0, -1);
        }

        if (y > 0) // the slope is for travelling to the right along x, but it can travel the opposite way.
            result = -result;

        return result;
    }
}
