﻿using UnityEngine;
using System.Collections;

namespace DevTipsAndresCartin.Util
{
    public static class RandomUtil
    {
        public static Vector3 Vector3(Vector3 minPosition, Vector3 maxPosition)
        {
            return new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                Random.Range(minPosition.z, maxPosition.z));
        }
    }
}