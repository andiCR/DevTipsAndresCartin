using System.Collections;
using UnityEngine;

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

        [HideInInspector] public ExitMode exitMode = ExitMode.Next;

        public IEnumerator Execute()
        {
            foreach (var obj in objectsToActivate)
                obj.gameObject.SetActive(true);

            gameObject.SetActive(true);

            yield return EnterAnim();
            Debug.Log("Entered " + gameObject.name);

            yield return WaitForEnd();

            Debug.Log("Exiting " + gameObject.name);

            yield return ExitAnim();

            gameObject.SetActive(false);

            foreach (var obj in objectsToActivate)
                obj.gameObject.SetActive(false);
        }

        public virtual IEnumerator EnterAnim()
        {
            yield return null;
        }

        public virtual IEnumerator ExitAnim()
        {
            yield return null;
        }

        public virtual IEnumerator WaitForEnd()
        {
            do
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    exitMode = ExitMode.Next;
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    exitMode = ExitMode.Previous;
                    break;
                }
                yield return null;
            } while (true);
        }
    }
}
