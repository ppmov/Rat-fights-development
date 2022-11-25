using UnityEngine;
using UnityEngine.Events;

namespace Utilities.InputSystem
{
    public class ColliderSelector : MonoBehaviour
    {
        [SerializeField]
        private int _mouseButton;
        [SerializeField]
        private LayerMask _layerMask;
        private Camera _camera;

        [Space]
        public UnityEvent<GameObject> OnSelected;

        private Vector2 MousePosition => Input.mousePosition;
        private bool IsSelectButtonUp => Input.GetMouseButtonUp(_mouseButton);

        private void Awake() => _camera = Camera.main;

        private void Update()
        {
            if (IsSelectButtonUp)
                SelectObjectAtScreenPosition(MousePosition, _layerMask.value);
        }

        public void SelectObjectAtScreenPosition(Vector3 screenPoint, int layerMask)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPoint), out RaycastHit hit, Mathf.Infinity, layerMask))
                OnSelected.Invoke(hit.collider.gameObject);
        }
    }
}