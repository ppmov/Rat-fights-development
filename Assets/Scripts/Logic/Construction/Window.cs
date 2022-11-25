using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities.UI;

namespace Logic.Construction
{
    [AddComponentMenu("RTS/Construction/Window")]
    public class Window : MonoBehaviour
    {
        [SerializeField]
        private ActionMenu _actionMenu;
        [SerializeField]
        private Sprite _cancelIcon;

        // each button will close action list
        private void Start() => _actionMenu.OnAnyButtonSelected.AddListener(Close);

        public void Select(GameObject gameObject)
        {
            var site = gameObject.GetComponent<Site>();
            if (site == null)
                return;

            // place action menu under mouse position
            _actionMenu.gameObject.SetActive(true);
            //_actionMenu.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // convert select action array to options dictionary
            Dictionary<Sprite, Action> options = new();

            foreach (var plan in site.availablePlans)
                options.Add(plan.icon, () => Constructor.Build(plan, site));

            // add cancel button
            options.Add(_cancelIcon, null);
            
            // set options
            _actionMenu.SetOptions(options);
        }

        public void Close() => _actionMenu.gameObject.SetActive(false);
    }
}
