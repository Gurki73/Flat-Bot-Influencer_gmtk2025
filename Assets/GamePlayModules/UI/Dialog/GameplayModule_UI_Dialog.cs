using Capsule.Core;
using System.Collections;
using UnityEngine;
using Talk;

namespace Capsule.UI
{
    public class GameplayModule_UI_Dialog : GameplayModule_UIBase
    {
        [SerializeField] private DialogData dialog;
        [SerializeField] private DialogHUD dialogHUD; // Your UI display logic
        private int currentLineIndex;

        public override void InitializeUI(int order)
        {
            currentLineIndex = 0;
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");
        }

        public void SetDialog(DialogData newDialog)
        {
            dialog = newDialog;
        }

        public override void OnEnter()
        {
            gameObject.SetActive(true);
            ShowCurrentLine();
        }

        private void ShowCurrentLine()
        {
            return;
            if (currentLineIndex >= dialog.Lines.Count)
            {
                ExitDialog();
                return;
            }

            var line = dialog.Lines[currentLineIndex];
            dialogHUD.ShowLine(line.Text, line.Speaker);

            if (line.HasChoices)
                dialogHUD.ShowChoices(line.Choices, OnChoiceSelected);
            else if (line.WaitForInput)
                StartCoroutine(WaitAndAdvance());
            else
                AdvanceDialog();
        }

        private void OnChoiceSelected(string selectedValue)
        {
            // Optionally store or pass value
            AdvanceDialog();
        }

        private IEnumerator WaitAndAdvance()
        {
            yield return new WaitForSeconds(2f);
            AdvanceDialog();
        }

        private void AdvanceDialog()
        {
            currentLineIndex++;
            ShowCurrentLine();
        }

        private void ExitDialog()
        {
            OnExit();
            GameStack.Instance.Pop(); // Use stack pop
        }

        public override void OnExit()
        {
            //  dialogHUD.Clear();
        }

        public void Skip()
        {
            var line = dialog.Lines[currentLineIndex];
            if (!line.HasChoices) AdvanceDialog();
        }
        public override void OnInput_UI(Inputs input)
        {
            AdvanceDialog(); // or let player skip
        }
    }
}
