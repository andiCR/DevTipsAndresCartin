using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DevTipsAndresCartin.Examples
{
    public class CoroutineExample : MonoBehaviour
    {
        public Canvas canvasToActivate;
        public Transform characterToMove;
        public Transform[] targetPositions;
        public float moveSpeed = 1.5f;

        private Vector3 _initPos;

        private void OnEnable()
        {
            _initPos = characterToMove.position;
            StartCoroutine(Execute());
        }

        private void OnDisable()
        {
            canvasToActivate.gameObject.SetActive(false);
            characterToMove.position = _initPos;
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(2);

            yield return WaitForMovement();

            canvasToActivate.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            canvasToActivate.gameObject.SetActive(false);
            characterToMove.transform.DOMove(_initPos, moveSpeed);
        }

        private IEnumerator WaitForMovement()
        {
            foreach (var targetPosition in targetPositions)
            {
                characterToMove.transform.DOMove(targetPosition.position, moveSpeed);
                yield return new WaitForSeconds(moveSpeed);
            }
        }
    }
}