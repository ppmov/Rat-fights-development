using UnityEngine;

namespace Utilities.InputSystem
{
    public class CursorFreezer : MonoBehaviour
    {
        public Vector2 DragDirection => (_fakeMousePosition - _previousFramePosition).normalized;

        private Vector2 _previousFramePosition;
        private Vector2 _fakeMousePosition;

        private Vector2 MousePosition => Input.mousePosition;

        private void OnEnable() => _previousFramePosition = _fakeMousePosition = MousePosition;

        private void Update() => _fakeMousePosition += MousePosition - _previousFramePosition;

        private void LateUpdate() => _previousFramePosition = MousePosition;
    }
}
