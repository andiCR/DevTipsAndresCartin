using System.Collections;
using UnityEngine;
using DG.Tweening;
using DevTipsAndresCartin.Util;

namespace DevTipsAndresCartin.Examples
{
    public class AssetStoreExample: MonoBehaviour
    {
        public GameObject[] fxPrefabs;

        public Transform max;
        public Transform min;
        public float timeToSpawn = 0.5f;
        public float timeToDestroyGfx = 3;

        public Transform transformToSpin;

        private void OnEnable()
        {
            StartCoroutine(Execute());
        }

        private void OnDisable()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube((max.position + min.position) / 2, max.position - min.position);
        }

        private IEnumerator Execute()
        {
            transformToSpin.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
            while (true)
            {
                foreach (var fx in fxPrefabs)
                {
                    var instance = Instantiate(fx, transform);
                    instance.transform.localPosition = RandomUtil.Vector3(min.localPosition, max.localPosition);
                    Destroy(instance, timeToDestroyGfx);
                    yield return new WaitForSeconds(timeToSpawn);
                }
            }
        }
    }
}
