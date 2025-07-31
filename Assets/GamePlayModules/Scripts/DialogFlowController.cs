using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;  // if needed

namespace Talk
{
    public class DialogFlowController : MonoBehaviour
    {
        public static DialogFlowController Instance { get; private set; }

        [SerializeField] private List<GameObject> dialogModules; // Dialog module prefabs or scene objects
        private int currentIndex = 0;

        public GameObject world;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PushNextDialogStep()
        {
            world.SetActive(true);

            foreach (GameObject go in dialogModules)
            {
                go.SetActive(false);
                GameStack.Instance.Pop();
            }

            if (currentIndex >= dialogModules.Count)
            {
                currentIndex = 0;
            }

            var moduleGO = dialogModules[currentIndex];
            var module = moduleGO.GetComponent<IGameplayModule>();
            if (module != null)
            {
                dialogModules[currentIndex].SetActive(true);
                GameStack.Instance.Push(module);
                currentIndex++;
            }
            else
            {
                Debug.LogWarning("Dialog module missing IGameplayModule component!");
            }
        }

        public void OnDialogStepComplete()
        {
            GameStack.Instance.Pop();
            PushNextDialogStep();
        }
    }
}