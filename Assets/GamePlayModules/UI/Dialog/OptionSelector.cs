using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Capsule.Core;

namespace Capsule.UI
{
    public class OptionSelector : MonoBehaviour
    {
        [Header("Option Buttons")]
        public Button[] optionButtons;
        public TextMeshProUGUI[] optionTexts;  // Text components inside buttons

        private int selectedIndex = 0;

        public event System.Action<int> OnOptionSelected;

        void Start()
        {
            for (int i = 0; i < optionButtons.Length; i++)
            {
                int index = i;  // local copy for closure
                optionButtons[i].onClick.AddListener(() => SelectOption(index));
            }
        }

        public void SetupOptions() // DialogAnswer[] options
        {
            // for (int i = 0; i < optionButtons.Length; i++)
            // {
            //     bool active = i < options.Length;
            //     optionButtons[i].gameObject.SetActive(active);
            //     if (active)
            //     {
            //         optionTexts[i].text = options[i].text;
            //     }
            // }

            selectedIndex = 0;
            UpdateSelectionVisuals();
        }
        public void OnNavigate(Vector2 direction)
        {
            // Round input to 4 cardinal directions
            if (direction == Vector2.up)
                selectedIndex = 0;
            else if (direction == Vector2.left)
                selectedIndex = 1;
            else if (direction == Vector2.down)
                selectedIndex = 2;
            else if (direction == Vector2.right)
                selectedIndex = 3;

            // Clamp to available options count
            if (selectedIndex >= optionButtons.Length || !optionButtons[selectedIndex].gameObject.activeSelf)
            {
                // Find first active button or fallback to 0
                for (int i = 0; i < optionButtons.Length; i++)
                {
                    if (optionButtons[i].gameObject.activeSelf)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }
            UpdateSelectionVisuals();
        }


        public void OnConfirm()
        {
            OnOptionSelected?.Invoke(selectedIndex);
        }

        public void SelectOption(int index)
        {
            selectedIndex = index;
            UpdateSelectionVisuals();
        }

        private void UpdateSelectionVisuals()
        {
            for (int i = 0; i < optionButtons.Length; i++)
            {
                ColorBlock colors = optionButtons[i].colors;
                colors.normalColor = (i == selectedIndex) ? Color.yellow : Color.white;
                optionButtons[i].colors = colors;
            }
        }
    }
}
