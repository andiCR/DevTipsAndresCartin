using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using DevTipsAndresCartin.Util;

namespace DevTipsAndresCartin.Examples
{
    public class MessengerExample: MonoBehaviour
    {
        public TextMeshProUGUI healthText;
        public GameObject bulletPrefab;
        public Transform max;
        public Transform min;
        public float bulletSpawnTime = 3;

        private void Start()
        {
            StartCoroutine(SpawnBullets());
        }

        private IEnumerator SpawnBullets()
        {
            while (true)
            {
                var bullet = Instantiate(bulletPrefab, transform);
                bullet.tag = "Item";
                var collider = bullet.AddComponent<BoxCollider>();
                collider.isTrigger = true;
                bullet.transform.localPosition = RandomUtil.Vector3(min.localPosition, max.localPosition);
                yield return new WaitForSeconds(bulletSpawnTime);
            }
        }

        private void OnEnable()
        {
            Messenger.Register("ItemsChanged", HandleItemsChanged);
        }


        private void OnDisable()
        {
            Messenger.Unregister("ItemsChanged", HandleItemsChanged);
        }

        private void HandleItemsChanged(BasicMessage e)
        {
            var health = ((GenericMessage<int>)e).Value;
            healthText.text = "HONGUILLOS: " + health;
            healthText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube((max.position + min.position) / 2, max.position - min.position);
        }

    }
}
