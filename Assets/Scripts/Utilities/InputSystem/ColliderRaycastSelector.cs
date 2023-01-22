using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Utilities.InputSystem
{
    [AddComponentMenu("Input/Collider Selector With Raycast")]
    public class ColliderRaycastSelector : MonoBehaviour
    {
        public enum SelectCondition { None, LeftButton, RightButton, MiddleButton }

        private Camera _camera;

        [SerializeField]
        private LayerMask _layerMask;
        [SerializeField]
        private SelectCondition _selectCondition;

        [Space]
        public UnityEvent<GameObject> hitEventObject;
        public UnityEvent<Vector3> hitEventPoint;

        private Vector2 MousePosition => Mouse.current.position.ReadValue();

        private void Awake() => _camera = Camera.main;

        private void Update()
        {
            switch (_selectCondition)
            {
                case SelectCondition.None:
                    break;

                case SelectCondition.LeftButton:
                    if (!Mouse.current.leftButton.isPressed)
                        return;
                    else
                        break;

                case SelectCondition.RightButton:
                    if (!Mouse.current.rightButton.isPressed)
                        return;
                    else
                        break;

                case SelectCondition.MiddleButton:
                    if (!Mouse.current.middleButton.isPressed)
                        return;
                    else
                        break;
            }

            SelectObjectAtMousePosition();
        }

        public void SelectObjectAtMousePosition()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(MousePosition), out RaycastHit hit, Mathf.Infinity, _layerMask.value))
            {
                hitEventObject.Invoke(hit.collider.gameObject);
                hitEventPoint.Invoke(hit.point);
            }
        }
    }
}