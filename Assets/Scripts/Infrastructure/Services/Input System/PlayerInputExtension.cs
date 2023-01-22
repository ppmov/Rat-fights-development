using Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

public partial class PlayerInput : IService
{
    public static Vector2 MousePosition => Mouse.current.position.ReadValue();

    private CursorFreezer _freezer;

    public bool IsCursorOffScreen(float xOffset = 0f, float yOffset = 0f) => 
        MousePosition.x.IsOutOfBounds(xOffset, Screen.width - xOffset) || MousePosition.y.IsOutOfBounds(yOffset, Screen.height - yOffset);

    public Vector2 GetMouseFromCenterDirection() => 
        new Vector2(MousePosition.x - Screen.width / 2f, MousePosition.y - Screen.height / 2f).normalized;

    public bool IsCursorFreezed(out Vector2 dragDirection)
    {
        dragDirection = Vector2.zero;

        if (Player.MiddleClick.IsPressed())
        {
            if (_freezer == null)
                _freezer = new();
        }
        else
        {
            if (_freezer != null)
                _freezer = null;
        }

        if (_freezer != null)
            dragDirection = _freezer.GetDragDirection();
        
        return _freezer != null;
    }

    private class CursorFreezer
    {
        private Vector2 _lastMousePosition;
        private Vector2 _fakeMousePosition;

        public CursorFreezer() => _lastMousePosition = _fakeMousePosition = MousePosition;

        public Vector2 GetDragDirection()
        {
            _fakeMousePosition += MousePosition - _lastMousePosition;
            var drag = (_fakeMousePosition - _lastMousePosition).normalized;
            _lastMousePosition = MousePosition;
            return drag;
        }
    }
}
