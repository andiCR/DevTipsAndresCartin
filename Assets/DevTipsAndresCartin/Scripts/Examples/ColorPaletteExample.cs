using System;
using System.Collections;
using DevTipsAndresCartin.Util;
using UnityEngine;

namespace DevTipsAndresCartin.Examples
{
    public class ColorPaletteExample: MonoBehaviour
    {
        public Renderer grass;
        public Color[] colors;
        public GameObject prefab;
        public float lifeTime;
        public float spawnTime;

        public Transform max;
        public Transform min;
        public void OnEnable()
        {
            grass.material.color = colors.RandomElement();
            StartCoroutine(SpawnCharacters());
        }

        private IEnumerator SpawnCharacters() 
        {
            while (true) 
            {
                var instance = Instantiate(prefab, transform);
                instance.transform.localPosition = RandomUtil.Vector3(min.localPosition, max.localPosition);
                instance.GetComponent<Renderer>().material.color = colors.RandomElement();
                Destroy(instance, lifeTime);
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}
