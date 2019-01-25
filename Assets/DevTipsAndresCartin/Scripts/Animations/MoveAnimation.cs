using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace DevTipsAndresCartin.Animations
{
    public class MoveAnimation : AnimationTween
    {
        public Vector3 from;
        public float duration;

        private Vector3 _to;

        private void Awake()
        {
            _to = transform.localPosition;
        }

        protected override Tweener PlayAnim()
        {
            transform.localPosition = from;
            return transform.DOLocalMove(_to, duration);
        }
    }
}