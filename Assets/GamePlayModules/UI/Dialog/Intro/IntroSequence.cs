using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Capsule.Core;
using Talk;
using TMPro;

namespace Capsule.UI
{
    public class IntroSequence : GameplayModule_UIBase
    {
        [Header("Characters")]
        public Image[] characterImages;
        public float characterFadeDuration = 0.5f;
        public float characterFadeDelay = 0.3f;

        [Header("Typewriter")]
        public RectTransform typewriterObject;
        public Vector2 typewriterExitDirection = new Vector2(500f, -300f); // now customizable
        public float typewriterExitDuration = 1.2f;
        public float typewriterExitDelay = 0.2f;

        [Header("Scrolling Text")]
        public RectTransform textMaskContainer; // add a UI Mask on this
        public RectTransform scrollTextContainer; // moving object inside mask
        public TextMeshProUGUI scrollText;
        [TextArea(3, 10)] public string[] lines;
        public float scrollSpeed = 30f;
        public float scrollDelayBetweenLines = 0.5f;

        [Header("AI Parts")]
        public RectTransform aiBody;
        public RectTransform aiHead;
        public RectTransform aiHair;
        public float partFlyInDuration = 0.8f;
        public float partFlyInDelay = 0.2f;

        [Header("Final AI Activation")]
        public GameObject aiFace;
        public float faceActivationDelay = 0.5f;

        public void Start()
        {
            // Ensure all AI parts are hidden before animating
            aiBody.gameObject.SetActive(false);
            aiHead.gameObject.SetActive(false);
            aiHair.gameObject.SetActive(false);
            aiFace.SetActive(false);
            StartCoroutine(ScrollTextSimple());
            StartCoroutine(RunIntroSequence());
        }

        public override void OnEnter()
        {

            // Ensure all AI parts are hidden before animating
            aiBody.gameObject.SetActive(false);
            aiHead.gameObject.SetActive(false);
            aiHair.gameObject.SetActive(false);
            aiFace.SetActive(false);
            StartCoroutine(ScrollTextSimple());
            StartCoroutine(RunIntroSequence());
        }

        [SerializeField] private RectTransform scrollMaskContainer; // with Mask
        [SerializeField] private float scrollDelay = 0.5f; // before scrolling starts

        private Vector2 originalPosition;

        private IEnumerator ScrollTextSimple()
        {
            // Set text
            scrollText.ForceMeshUpdate();

            // Reset position
            originalPosition = scrollTextContainer.anchoredPosition;
            scrollTextContainer.anchoredPosition = originalPosition;

            // Wait before scrolling
            yield return new WaitForSeconds(scrollDelay);

            // Measure total scroll distance
            float contentHeight = scrollText.preferredHeight;
            float viewHeight = scrollMaskContainer.rect.height;
            float scrollAmount = Mathf.Max(0, contentHeight - viewHeight + 50f); // add a little padding at the bottom

            float duration = scrollAmount / scrollSpeed;
            float timer = 0f;

            while (timer < duration)
            {
                float y = Mathf.Lerp(0, scrollAmount, timer / duration);
                scrollTextContainer.anchoredPosition = originalPosition + new Vector2(0, y);
                timer += Time.deltaTime;
                yield return null;
            }

            scrollTextContainer.anchoredPosition = originalPosition + new Vector2(0, scrollAmount);
        }


        public override void OnExit() { }

        public override void InitializeUI(int order) { }

        private IEnumerator RunIntroSequence()
        {
            // Fade out characters one by one with overlap
            for (int i = 0; i < characterImages.Length; i++)
            {
                StartCoroutine(FadeOutImage(characterImages[i], characterFadeDuration));
                yield return new WaitForSeconds(characterFadeDelay);
            }

            yield return new WaitForSeconds(0.5f);

            // Typewriter flies out (in reverse direction)
            yield return new WaitForSeconds(typewriterExitDelay);
            StartCoroutine(FlyOutTypewriter());

            // Scroll text
            yield return StartCoroutine(ScrollText());

            // Fly in AI parts
            yield return StartCoroutine(FlyIn(aiBody, new Vector2(-800, 0)));
            yield return new WaitForSeconds(partFlyInDelay);
            yield return StartCoroutine(FlyIn(aiHead, new Vector2(800, 0)));
            yield return new WaitForSeconds(partFlyInDelay);
            yield return StartCoroutine(FlyIn(aiHair, new Vector2(0, 500)));

            yield return new WaitForSeconds(faceActivationDelay);
            aiFace.SetActive(true);
            TriggerNextStep();
        }

        private void TriggerNextStep()
        {
            Debug.Log("✅ Intro finished → starting first dialog...");

            gameObject.SetActive(false);
            GameStack.Instance.Pop();

            DialogFlowController.Instance?.PushNextDialogStep();
        }

        public void Skip()
        {
            TriggerNextStep();
        }

        private IEnumerator FadeOutImage(Image img, float duration)
        {
            Color original = img.color;
            float t = 0;
            while (t < duration)
            {
                float a = Mathf.Lerp(1, 0, t / duration);
                img.color = new Color(original.r, original.g, original.b, a);
                t += Time.deltaTime;
                yield return null;
            }
            img.color = new Color(original.r, original.g, original.b, 0);
        }

        private IEnumerator FlyOutTypewriter()
        {
            Vector3 start = typewriterObject.anchoredPosition;
            Vector3 end = start + (Vector3)typewriterExitDirection;
            float t = 0;

            while (t < typewriterExitDuration)
            {
                float normalized = t / typewriterExitDuration;
                typewriterObject.anchoredPosition = Vector3.Lerp(start, end, normalized);
                typewriterObject.rotation = Quaternion.Euler(0, 0, normalized * 360);
                t += Time.deltaTime;
                yield return null;
            }

            typewriterObject.gameObject.SetActive(false);
        }

        private IEnumerator ScrollText()
        {
            // Prepare full text
            string full = "";
            foreach (var line in lines)
                full += line + "\n";

            scrollText.text = full;

            // Reset position
            scrollTextContainer.anchoredPosition = Vector2.zero;

            float contentHeight = scrollText.preferredHeight;
            Vector2 start = scrollTextContainer.anchoredPosition;
            Vector2 end = start + new Vector2(0, contentHeight + 50f); // scroll upward

            float duration = contentHeight / scrollSpeed;
            float t = 0;

            yield return new WaitForSeconds(scrollDelayBetweenLines);

            while (t < duration)
            {
                float y = Mathf.Lerp(start.y, end.y, t / duration);
                scrollTextContainer.anchoredPosition = new Vector2(start.x, y);
                t += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator FlyIn(RectTransform part, Vector2 fromOffset)
        {
            part.gameObject.SetActive(true);

            Vector2 target = part.anchoredPosition;
            part.anchoredPosition = target + fromOffset;

            float t = 0;
            while (t < partFlyInDuration)
            {
                part.anchoredPosition = Vector2.Lerp(target + fromOffset, target, t / partFlyInDuration);
                t += Time.deltaTime;
                yield return null;
            }

            part.anchoredPosition = target;

        }
    }
}
