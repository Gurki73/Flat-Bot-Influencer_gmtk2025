using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Capsule.Core;
using Talk;

namespace Capsule.UI
{
    public class DialogHUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text speakerText;
        [SerializeField] private TMP_Text dialogText;
        [SerializeField] private Transform choiceContainer;
        [SerializeField] private Button choiceButtonPrefab;

        private Action<string> onChoiceSelected;

        public void ShowLine(string text, string speaker)
        {
            speakerText.text = speaker;
            dialogText.text = text;
            ClearChoices(); // remove any previous buttons
        }

        public void ShowChoices(List<DialogChoice> choices, Action<string> onChoice)
        {
            ClearChoices();
            onChoiceSelected = onChoice;

            foreach (var choice in choices)
            {
                var button = Instantiate(choiceButtonPrefab, choiceContainer);
                button.GetComponentInChildren<TMP_Text>().text = choice.Label;
                string choiceValue = choice.Value;

                button.onClick.AddListener(() =>
                {
                    onChoiceSelected?.Invoke(choiceValue);
                });
            }
        }

        public void Clear()
        {
            dialogText.text = "";
            speakerText.text = "";
            ClearChoices();
        }

        private void ClearChoices()
        {
            foreach (Transform child in choiceContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

