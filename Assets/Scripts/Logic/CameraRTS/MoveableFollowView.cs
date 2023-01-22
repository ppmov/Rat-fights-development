using UnityEngine;

namespace Logic.CameraRTS
{
    [AddComponentMenu("RTS/Camera/Moveable Follow View")]
    public class MoveableFollowView : FollowView
    {
        [Space]
        [Header("Movement setup")]
        [SerializeField]
        private float _axisMovementVelocity;
        [SerializeField]
        private float _mouseMovementVelocity;
        [SerializeField]
        private float _rotationVelocity;

        [Space]
        [SerializeField]
        private bool _isWheelMoveInversionEnabled;
        [SerializeField]
        private Vector2 _outOfWindowOffset;

        private bool HasToFollow { get; set; }
        private float FovFactor => FovBounds.y == 0 ? 1f : Camera.fieldOfView / FovBounds.y;

        private void Start()
        {
            UpdateFollowing(); // set correct initial transform
            Input.Player.CameraBinding.performed += (x) => HasToFollow = !HasToFollow;
        }

        private void OnDisable() => HasToFollow = false;

        private void Update() => UpdateScrolling();

        private void LateUpdate()
        {
            Move(Input.Player.Move.ReadValue<Vector2>(), _axisMovementVelocity);

            if (Input.IsCursorFreezed(out var dragDirection))
                Move(dragDirection, _mouseMovementVelocity * (_isWheelMoveInversionEnabled ? -1 : 1));
            else 
            if (Input.IsCursorOffScreen(_outOfWindowOffset.x, _outOfWindowOffset.y))
                Move(Input.GetMouseFromCenterDirection(), _mouseMovementVelocity);

            if (Input.Player.RotateLeft.IsPressed())
                Rotate(-1f, _rotationVelocity);
            else
            if (Input.Player.RotateRight.IsPressed())
                Rotate(1f, _rotationVelocity);

            if (HasToFollow)
                UpdateFollowing();
        }

        private void Move(Vector2 direction, float velocity)
        {
            if (direction == Vector2.zero)
                return;

            HasToFollow = false;
            Vector3 correctDirection = transform.right * direction.x + transform.up * direction.y;
            transform.position += velocity * Time.deltaTime * FovFactor * correctDirection;
        }

        private void Rotate(float direction, float velocity)
        {
            if (direction == 0f)
                return;

            HasToFollow = false;

            Vector3 center = new(_followed.transform.position.x, Camera.transform.position.y, _followed.transform.position.z);
            transform.RotateAround(center, Vector3.up, velocity * Time.deltaTime * direction);
        }
    }
}
