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
                var slides = new List<Slide>();
                foreach (Transform child in transform)
                {
                    slides.Add(child.GetComponent<Slide>());
                    child.gameObject.SetActive(false);
                }
                this.slides = slides.OrderBy(s => s.name).ToArray();
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