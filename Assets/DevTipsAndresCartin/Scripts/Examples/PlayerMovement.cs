using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevTipsAndresCartin.Examples
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        public int items = 0;

        private readonly float _interpolation = 10;
        private float _currentV = 0;
        private float _currentH = 0;
        private Vector3 _currentDirection = Vector3.zero;

        private void OnEnable()
        {
            items = 0;
            NotifyHealthChange();
        }

        private void NotifyHealthChange()
        {
            Messenger.Notify("ItemsChanged", new GenericMessage<int>(items));
        }

        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Transform camera = Camera.main.transform;

            _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * _interpolation);
            _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * _interpolation);

            Vector3 direction = camera.forward * _currentV + camera.right * _currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                _currentDirection = Vector3.Slerp(_currentDirection, direction, Time.deltaTime * _interpolation);

                transform.rotation = Quaternion.LookRotation(_currentDirection);
                transform.position += _currentDirection * speed * Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("TRIGGER ENTER- " + other.tag);
            if (other.CompareTag("Item"))
            {
                Destroy(other.gameObject);
                items++;
                NotifyHealthChange();
            }
        }
    }
}