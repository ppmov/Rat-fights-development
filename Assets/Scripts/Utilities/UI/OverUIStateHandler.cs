using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    [AddComponentMenu("Event/Pointer over UI states handler")]
    [RequireComponent(typeof(EventSystem))]
    public class OverUIStateHandler : MonoBehaviour
    {
        [Header("Calls when player pointer enters over any UI element")]
        public UnityEvent OnUIEnter;
        [Header("Calls when player pointer exits from any UI element")]
        public UnityEvent OnUIExit;

        private bool lastState;
        private EventSystem _eventSystem;
        
        private bool IsPointerOverUI => _eventSystem.IsPointerOverGameObject();

        private void Awake() => _eventSystem = GetComponent<EventSystem>();

        private void Update()
        {
            if (lastState != IsPointerOverUI)
            {
                (lastState ? OnUIExit : OnUIEnter).Invoke();
                lastState = !lastState;
            }
        }
    }
}
