using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace DevTipsAndresCartin
{
    public class Slide: MonoBehaviour
    {
        public enum ExitMode
        {
            Next,
            Previous
        }

        public GameObject[] objectsToActivate;

        [HideInInspector] public bool canGoBack = true;
        [HideInInspector] public bool canGoForward = true;
        [HideInInspector] public ExitMode exitMode = ExitMode.Next;

        protected CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator Execute()
        {
            foreach (var obj in objectsToActivate)
                obj.gameObject.SetActive(true);

            yield return EnterAnim();

            yield return WaitForEnd();

            yield return ExitAnim();

            foreach (var obj in objectsToActivate)
                obj.gameObject.SetActive(false);
        }

        public virtual IEnumerator EnterAnim()
        {
            gameObject.SetActive(true);
            const float fadeTime = 0.1f;
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, fadeTime);
            yield return null;
        }

        public virtual IEnumerator ExitAnim()
        {
            const float fadeTime = 0.4f;
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0, fadeTime).OnComplete(()=> gameObject.SetActive(false));

            yield return null;
        }

        public virtual IEnumerator WaitForEnd()
        {
            return WaitForInput();
        }

        protected virtual IEnumerator WaitForInput()
        {
            do
            {
                if (canGoForward && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    exitMode = ExitMode.Next;
                    break;
                }
                else if (canGoBack && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    exitMode = ExitMode.Previous;
                    break;
                }
                yield return null;
            } while (true);
        }
    }
}
