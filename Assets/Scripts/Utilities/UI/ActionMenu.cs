using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utilities.UI
{
    [AddComponentMenu("UI/ActionMenu")]
    public class ActionMenu : MonoBehaviour
    {
        [SerializeField]
        private float _distanceBetweenItemEdges;

        [SerializeField]
        private RectTransform _window;
        [SerializeField]
        private RectTransform _content;
        [SerializeField]
        private RectTransform _template;

        public UnityEvent OnAnyButtonSelected;

        private void Start() => UpdateHeight();

        public void SetOptions(Dictionary<Sprite, Action> options)
        {
            int index = 1;
            _template.gameObject.SetActive(true);

            foreach (var option in options)
            {
                var child = GetButton(index);
                child.onClick.RemoveAllListeners();

                child.image.sprite = option.Key;
                child.onClick.AddListener(() => option.Value?.Invoke());
                child.onClick.AddListener(() => OnAnyButtonSelected?.Invoke());
                index++;
            }

            if (index < _content.childCount)
                for (int i = _content.childCount - 1; i >= index; i--)
                {
                    var child = _content.GetChild(i);
                    child.parent = null;
                    Destroy(child.gameObject);
                }

            _template.gameObject.SetActive(false);
            UpdateHeight();
        }

        private Button GetButton(int index)
        {
            if (_content.childCount > index)
                return _content.GetChild(index).GetComponent<Button>();
            else
                return Instantiate(_template, _content).GetComponent<Button>();
        }

        private void UpdateHeight()
        {
            int realChildCount = _content.childCount - 1;
            float dbi = _template.sizeDelta.y + _distanceBetweenItemEdges;
            _window.sizeDelta = new Vector2(_window.sizeDelta.x, dbi * realChildCount);

            for (int i = 1; i < _content.childCount; i++)
            {
                RectTransform child = (RectTransform)_content.GetChild(i);
                child.anchoredPosition = new Vector2(0f, dbi * (0.5f - i));
            }

            // dbi - 50, size - 40
            // 1 - -25  / 50
            // 2 - -75  / 100
            // 3 - -125 / 150
        }
    }
}