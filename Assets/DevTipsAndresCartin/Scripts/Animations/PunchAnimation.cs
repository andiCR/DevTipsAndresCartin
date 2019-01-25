using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace DevTipsAndresCartin.Animations
{
    public class PunchAnimation : AnimationTween
    {
        public enum PunchType
        {
            Rotation,
            Scale,
            Position
        }

        public PunchType type;
        public Vector3 punch;
        public float duration;
        public int vibrato = 10;
        public float elasticity = 1;
        public bool snapping = false;

        protected override Tweener PlayAnim()
        {
            switch (type)
            {
                case PunchType.Position:
                    return transform.DOPunchPosition(punch, duration, vibrato, elasticity, snapping);
                case PunchType.Rotation:
                    return transform.DOPunchRotation(punch, duration, vibrato, elasticity);
                case PunchType.Scale:
                    return transform.DOPunchScale(punch, duration, vibrato, elasticity);
            }
            return null;
        }
    }
}