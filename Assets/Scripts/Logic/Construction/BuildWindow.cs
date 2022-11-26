using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities.UI;

namespace Logic.Construction
{
    [AddComponentMenu("RTS/Construction/Window")]
    public class BuildWindow : MonoBehaviour
    {
        [SerializeField]
        private RoundedActionMenu _actionMenu;
        [SerializeField]
        private Sprite _cancelIcon;

        //[Space]
        //public UnityEvent onOpened;
        //public UnityEvent onClosed;

        // should be called after selecting build site game object
        public void Select(GameObject gameObject)
        {
            var site = gameObject.GetComponent<BuildSite>();
            if (site != null)
                Select(site);
        }

        public void Select(BuildSite site)
        {
            if (site.availablePlans.Length == 0)
                return;

            List<RoundedActionMenu.Option> options = new();

            foreach (var plan in site.availablePlans)
                options.Add(new(plan.icon, () => Constructor.Build(plan, site)));

            // add cancel button
            options.Add(new(_cancelIcon, null));
            gameObject.SetActive(true);
            _actionMenu.SetOptions(options);
        }

        public void Close() => gameObject.SetActive(false);
    }
}
