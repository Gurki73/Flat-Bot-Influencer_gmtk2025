using UnityEngine;
using Capsule.Core;

namespace Capsule.UI
{
    public class GameplayModule_UI_Pause : GameplayModule_UIBase
    {
        [Header("Visuals")]
        public CanvasGroup canvasGroup;  // Assign via prefab
        public GameObject fxContainer;   // Optional for steam/smoke

        public override void InitializeUI(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
            }

            if (fxContainer != null)
                fxContainer.SetActive(false);
        }

        public override void OnEnter()
        {
            Debug.Log("pause active " + gameObject.name);

            gameObject.SetActive(true); // <- This enables the GameObject and all attached components.

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }

            if (fxContainer != null)
                fxContainer.SetActive(true);
        }

        public override void OnExit()
        {
            Time.timeScale = 1f;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            if (fxContainer != null)
                fxContainer.SetActive(false);

            gameObject.SetActive(false); // <- This disables the GameObject (and will prevent Update, etc.).
        }


        public override void OnInput(Inputs input)
        {
            if (input == Inputs.Pause) return;
            GameStack.Instance.Pop(); // Or whatever method exits this panel
        }
    }
}
