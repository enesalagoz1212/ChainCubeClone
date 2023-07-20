using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;
using ChainCube.Controllers;

namespace ChainCube.Platforms
{
    public class CubeResetZone : MonoBehaviour
    {
        public BoxCollider boxCollider;
        
        private void OnEnable()
        {
            GameManager.OnGameStarted += OnGameStart;
            GameManager.OnGameEnd += OnGameEnd;
            GameManager.OnGameReset += OnGameReset;
        }
        private void OnDisable()
        {
            GameManager.OnGameStarted -= OnGameStart;
            GameManager.OnGameEnd -= OnGameEnd;
            GameManager.OnGameReset -= OnGameReset;
        }

        private void OnGameStart()
        {
            boxCollider.enabled = true;
        }
        
        private void OnGameEnd()
        {
            boxCollider.enabled = false;
        }
        
        private void OnGameReset()
        {
            boxCollider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                CubeController cubeController = other.GetComponent<CubeController>();
                OnCubeTriggered(cubeController);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                CubeController cubeController = other.GetComponent<CubeController>();
                OnCubeTriggered(cubeController);
            }
        }

        private void OnCubeTriggered(CubeController cubeController)
        {
            if (cubeController != null && cubeController.IsEndTriggerAvailable)
            {
                Debug.Log("trigger1");
                cubeController.IsEndTriggerAvailable = false;
                LevelManager.Instance.OnCubeCollidedWithReset(cubeController);
            }
        }
    }
}