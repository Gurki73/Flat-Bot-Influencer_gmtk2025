using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;  // if needed
using Capsule.UI;
using CameraCrew;

namespace Talk
{
    public class DialogFlowController : MonoBehaviour
    {
        public Camera cam;
        public static DialogFlowController Instance { get; private set; }

        public Vector3 cameraPosAi = new Vector3(-15, 0, -10);
        public Vector3 cameraPosProducerGuy = new Vector3(15, 0, -10);
        public Vector3 cameraPosDialog = new Vector3(0, 0, -10);

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
            var module = moduleGO.GetComponent<GameplayModule_UIBase>();
            if (module != null)
            {
                Vector3 camPos;
                switch (module.speakerType)
                {
                    case (GameplayModule_UIBase.SpeakerType.AI):
                        camPos = cameraPosAi;
                        break;
                    case (GameplayModule_UIBase.SpeakerType.Producer):
                        camPos = cameraPosProducerGuy;
                        break;
                    default:
                        camPos = cameraPosDialog;
                        break;
                }
                cam.GetComponent<CameraMover>().MoveCameraRoutine(camPos);
                IGameplayModule igm = moduleGO.GetComponent<IGameplayModule>();
                dialogModules[currentIndex].SetActive(true);
                GameStack.Instance.Push(igm);
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