using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Managers;
using ChainCube.Controllers;

namespace ChainCube.Platforms
{
    public class CubeResetZone : MonoBehaviour
    {
		
		private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                Debug.Log("trigger");
                CubeController cubeController = other.GetComponent<CubeController>();
                if (cubeController != null && cubeController.transform.position.z > transform.position.z)
                {
                    Debug.Log("trigger1");
                    LevelManager.Instance.OnCubeCollidedWithReset(cubeController);
                    UIManager.Instance.OnCubeCollidedWithReset();
                   
                }
            }
        }
    }
}

