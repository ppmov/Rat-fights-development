using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UI;

namespace Logic.Construction
{
    [AddComponentMenu("RTS/Construction/Window")]
    public class BuildWindow : MonoBehaviour
    {
        [SerializeField]
        private RoundedActionMenu _actionMenu;
        [SerializeField]
        private Button _cancelButton;

        private Camera _camera;
        private RectTransform _rect;
        private BuildSite _selectedSite;

        private BuildSite Selected
        {
            get => _selectedSite;
            set
            {
                gameObject.SetActive(value != null);
                _selectedSite = value;

                if (!value)
                {
                    _actionMenu.SetOptions(new());
                    _cancelButton.onClick.RemoveAllListeners();
                }
            }
        }

        private void Awake()
        {
            _camera = Camera.main;
            _rect = GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            if (Selected == null)
                return;

            // Selected following
            _rect.anchoredPosition = _camera.WorldToScreenPoint(Selected.transform.position);
        }

        // unity event target
        public void Select(GameObject gameObject)
        {
            var site = gameObject.GetComponent<BuildSite>();

            if (site != null)
                Select(site);
        }

        public void Select(BuildSite site)
        {
            if (!site.CanBeCanceled && site.AvailablePlans.Length == 0)
                return;

            // cancel build button
            _cancelButton.onClick.AddListener(() => site.Cancel());

            // setup plan buttons
            List<RoundedActionMenu.Option> options = new();

            foreach (var plan in site.AvailablePlans)
                options.Add(new(plan.icon, () => site.Build(plan)));

            _actionMenu.SetOptions(options);

            // enabling
            _cancelButton.gameObject.SetActive(site.CanBeCanceled);
            _actionMenu.gameObject.SetActive(options.Count > 0);
            Selected = site;
        }

        public void Close() => Selected = null;
    }
}
