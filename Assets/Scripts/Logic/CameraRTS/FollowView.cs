using Infrastructure.Services;
using UnityEngine;

namespace Logic.CameraRTS
{
    [AddComponentMenu("RTS/Camera/Follow View")]
    [RequireComponent(typeof(Camera))]
    public class FollowView : MonoBehaviour
    {
        public Vector2 FovBounds => _fieldOfViewBounds;

        [SerializeField]
        protected Transform _followed;
        [SerializeField]
        private bool _isometric;

        [Header("Relative transform")]
        [SerializeField]
        private float _rotationAngleX;
        [SerializeField]
        private int _distance;
        [SerializeField]
        private float _offsetY;

        [Header("Zooming")]
        [SerializeField]
        private float _scrollingVelocity;
        [SerializeField]
        private Vector2 _fieldOfViewBounds;

        protected Camera Camera { get; private set; }
        protected PlayerInput Input { get; private set; }

        protected virtual void Awake()
        {
            Camera = GetComponent<Camera>();
            Input = SL.Single<PlayerInput>();
        }

        private void Start() => UpdateFollowing(); // set correct initial transform
        private void Update() => UpdateScrolling();
        private void LateUpdate() => UpdateFollowing();

        public void CalcExpectedTransform(out Vector3 position, out Quaternion rotation)
        {
            Vector3 followingPosition = _followed.position;
            followingPosition.y += _offsetY;

            if (_isometric)
                // tilt only
                rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            else
                // repeats followed rotation + tilt
                rotation = Quaternion.LookRotation(_followed.transform.forward) * Quaternion.Euler(_rotationAngleX, 0, 0);

            position = rotation * new Vector3(0, 0, -_distance) + followingPosition;
        }

        protected void UpdateFollowing()
        {
            if (_followed == null)
                return;

            CalcExpectedTransform(out Vector3 position, out Quaternion rotation);
            transform.SetPositionAndRotation(position, rotation);
        }

        protected void UpdateScrolling() => Zoom(Input.Player.Scroll.ReadValue<Vector2>().y, _scrollingVelocity);

        private void Zoom(float direction, float velocity) => 
            Camera.fieldOfView = Mathf.Clamp(Camera.fieldOfView - velocity * Time.deltaTime * Mathf.Clamp(direction, -1f, 1f), FovBounds.x, FovBounds.y);
    }
}
