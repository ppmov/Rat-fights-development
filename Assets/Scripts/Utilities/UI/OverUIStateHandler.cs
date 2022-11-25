using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    [RequireComponent(typeof(EventSystem))]
    public class OverUIStateHandler : MonoBehaviour
    {
        public UnityEvent OnUIEnter;
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
