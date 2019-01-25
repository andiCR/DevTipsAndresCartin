using System.Collections;
using UnityEngine;

namespace DevTipsAndresCartin.Examples
{
    public class AssetStoreExample: MonoBehaviour
    {
        public GameObject[] fxPrefabs;

        public Transform max;
        public Transform min;
        public float timeToSpawn = 0.5f;
        public float timeToDestroyGfx = 3;

        private void OnEnable()
        {
            StartCoroutine(Execute());
        }

        private void OnDisable()
        {
        }

        private IEnumerator Execute()
        {
            foreach (var fx in fxPrefabs)
            {
                var instance = Instantiate(fx, transform);
                Vector3 v = max.localPosition - min.localPosition;
                instance.transform.localPosition = min.localPosition + Random.value * v;
                Destroy(instance, timeToDestroyGfx);
                yield return new WaitForSeconds(timeToSpawn);
            }
        }

    }
}
