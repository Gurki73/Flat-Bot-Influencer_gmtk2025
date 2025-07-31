using UnityEngine;
using UnityEngine.UI;
using Capsule.Core;
using Talk;

namespace Capsule.UI
{
    public class GameplayModule_UI_MainMenu : GameplayModule_UIBase
    {
#if UNITY_EDITOR
        public Button debugResetPrefsButton;
#endif

        [Header("Main Menu UI")]
        public Button resumeButton;
        public Button newGameButton;
        public Button audioSettingsButton;
        public Button controlsButton;
        public GameObject settingsPanel;
        public GameObject controlsPanel;

        [SerializeField] private string newGameKey = "HasStartedGame";
        [SerializeField] private GameObject introObject;
        private IGameplayModule intro;

        [SerializeField] private GameObject audioMenuObject;
        private IGameplayModule audioMenu;

        public override string ModuleName => "MainMenu";
        public override ModulePriority Priority => priority;

        public override void InitializeUI(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");

            if (resumeButton != null)
                resumeButton.onClick.AddListener(OnResume);

            if (newGameButton != null)
                newGameButton.onClick.AddListener(OnNewGame);

            if (audioSettingsButton != null)
                audioSettingsButton.onClick.AddListener(ShowSettings);

            if (controlsButton != null)
                controlsButton.onClick.AddListener(ShowControls);

            if (audioMenuObject != null)
                audioMenu = audioMenuObject.GetComponent<IGameplayModule>();

            if (introObject != null)
                intro = introObject.GetComponent<IGameplayModule>();

#if UNITY_EDITOR
            if (debugResetPrefsButton != null)
            {
                debugResetPrefsButton.onClick.AddListener(ClearSavedProgress);
                OnEnter();
            }
#endif
        }

        void ClearSavedProgress()
        {
            PlayerPrefs.DeleteKey("HasStartedGame");
            PlayerPrefs.Save();
            Debug.Log("🧹 Cleared saved game progress (HasStartedGame)");
        }

        public override void OnEnter()
        {
            Debug.Log("🟦 Entered MainMenu: " + gameObject.name);
            gameObject.SetActive(true);
            Time.timeScale = 0f;

            bool hasProgress = PlayerPrefs.HasKey("HasStartedGame");
            Debug.Log("[MainMenu] has progress: " + hasProgress);
            // Show both buttons, but disable one of them based on progress
            newGameButton.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(true);

            newGameButton.interactable = !hasProgress;
            resumeButton.interactable = hasProgress;
        }


        public override void OnExit()
        {

        }

        void OnResume()
        {
            GameStack.Instance.Pop();
        }

        void OnNewGame()
        {
            GameStack.Instance.Clear();
            Debug.Log("🆕 New Game started → requesting genre...");

            PlayerPrefs.SetInt(newGameKey, 1);
            PlayerPrefs.Save();

            if (intro != null)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
                introObject.gameObject.SetActive(true);
                GameStack.Instance.Pop();
                GameStack.Instance.Push(intro);
            }

        }

        void ShowSettings()
        {
            if (audioMenu != null)
            {
                audioMenuObject.SetActive(true);
                OnGamePaused();
                GameStack.Instance.Push(audioMenu);
            }
            else
            {
                settingsPanel?.SetActive(true);
                controlsPanel?.SetActive(false);
            }
        }

        void ShowControls()
        {
            controlsPanel?.SetActive(true);
            settingsPanel?.SetActive(false);
        }
    }
}
