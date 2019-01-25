using DG.Tweening;
using EasyButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DevTipsAndresCartin.Animations
{
    public abstract class AnimationTween: MonoBehaviour
    {
        public bool playOnEnable;
        public float delay;
        public Ease easing;

        private void OnEnable()
        {
            if (playOnEnable)
                Play();
        }

        protected void Play()
        {
            var tweener = PlayAnim();
            if (tweener != null)
                tweener.SetDelay(delay).SetEase(easing);
        }

        protected abstract Tweener PlayAnim();

        [Button]
        public void TestAnim()
        {
            PlayAnim();
        }
    }
}
