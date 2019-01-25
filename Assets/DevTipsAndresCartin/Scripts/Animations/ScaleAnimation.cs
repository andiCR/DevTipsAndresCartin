using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace DevTipsAndresCartin.Animations
{
    public class ScaleAnimation: AnimationTween
    {
        public Vector3 startValue;
        public bool useCurrentScaleAsEndValue = true;
        public Vector3 endValue;
        public float duration;

        private void Awake()
        {
            if (useCurrentScaleAsEndValue)
                endValue = transform.localScale;
        }

        protected override Tweener PlayAnim()
        {
            transform.localScale = startValue;
            return transform.DOScale(endValue, duration);
        }
    }
}
