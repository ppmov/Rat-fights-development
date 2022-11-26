using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utilities.UI
{
    [AddComponentMenu("UI/Rounded Action Menu")]
    public class RoundedActionMenu : MonoBehaviour
    {
        [SerializeField]
        private float _distanceFromCenter;

        [Space]
        [SerializeField]
        private RectTransform _content;
        [SerializeField]
        private RectTransform _template;

        private void Start() => UpdateHeight();

        public void SetOptions(List<Option> options)
        {
            int index = 1;
            _template.gameObject.SetActive(true); // template is an instantiate original, so it should be active

            // change or create buttons
            foreach (var option in options)
            {
                var child = GetOrCreateButton(index);
                child.onClick.RemoveAllListeners();

                child.image.sprite = option.sprite;
                child.onClick.AddListener(() => option.action?.Invoke());
                index++;
            }

            // remove unnecessary
            if (index < _content.childCount)
                for (int i = _content.childCount - 1; i >= index; i--)
                {
                    var child = _content.GetChild(i);
                    child.SetParent(null);
                    Destroy(child.gameObject);
                }

            _template.gameObject.SetActive(false);
            UpdateHeight();
        }

        private Button GetOrCreateButton(int index)
        {
            if (_content.childCount > index)
                return _content.GetChild(index).GetComponent<Button>();
            else
                return Instantiate(_template, _content).GetComponent<Button>();
        }

        private void UpdateHeight()
        {
            int realChildCount = _content.childCount - 1; // template does not count
            float deltaAngle = 360f / realChildCount;

            for (int i = 1; i < _content.childCount; i++)
            {
                var child = (RectTransform)_content.GetChild(i);
                float angle = (i - 1) * deltaAngle * Mathf.Deg2Rad;
                child.anchoredPosition = new(_distanceFromCenter * Mathf.Cos(angle), _distanceFromCenter * Mathf.Sin(angle));
            }
        }

        public class Option
        {
            public Sprite sprite;
            public Action action;

            public Option(Sprite sprite, Action action)
            {
                this.sprite = sprite;
                this.action = action;
            }
        }
    }
}