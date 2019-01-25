using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevTipsAndresCartin
{
    public class Presentation: MonoBehaviour
    {
        public Slide[] slides;
        public int slideIndex;

        public Slide CurrentSlide { get { return slides[slideIndex]; } }
        public int SlideIndex
        {
            get { return slideIndex; }
            set { slideIndex = Mathf.Clamp(value, 0, slides.Length - 1); }
        }

        private void Start()
        {
            if (slides.Length == 0)
            {
                var slidesList = new List<Slide>();
                foreach (Transform child in transform)
                {
                    var slide = child.GetComponent<Slide>();
                    slidesList.Add(slide);
                    child.gameObject.SetActive(false);
                }
                slides = slidesList.ToArray();
                //slides = slidesList.OrderBy(s => s.name).ToArray();

                if (slides.Length > 0)
                {
                    slides[0].canGoBack = false;
                    slides[slides.Length - 1].canGoForward = false;
                }
            }
            StartCoroutine(SlideShow());
        }

        private IEnumerator SlideShow()
        {
            while (slideIndex < slides.Length)
            {
                yield return CurrentSlide.Execute();
                switch (CurrentSlide.exitMode)
                {
                    case Slide.ExitMode.Next:
                        SlideIndex++;
                        break;
                    case Slide.ExitMode.Previous:
                        SlideIndex--;
                        break;
                }
            }
        }
    }
}