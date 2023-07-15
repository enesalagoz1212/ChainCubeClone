using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;
using ChainCube.Controllers;

namespace ChainCube.Platforms
{
    public class Reset : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // if (other.gameObject.CompareTag("Cube"))
            // {
            //     GameManager.OnGameReset?.Invoke();
            // }
        }
    }
}

