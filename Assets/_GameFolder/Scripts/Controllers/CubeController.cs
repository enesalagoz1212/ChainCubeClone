using System;
using System.Collections;
using System.Collections.Generic;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using UnityEngine;
using TMPro;

namespace ChainCube.Controllers
{
    public class CubeController : MonoBehaviour
    {
        public TextMeshPro[] cubeTexts;
        public CubeData cubeData;

        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void CubeCreated()
        {
            cubeData = CubeDataManager.Instance.ReturnRandomCubeData();
            UpdateCubeText();
        }
        
        private void UpdateCubeText()
        {
            _meshRenderer.material.color = cubeData.color;
            foreach (TextMeshPro item in cubeTexts)
            {
                item.text = cubeData.number.ToString();
            }
        }

        public void ThrowCube()
        {
            _rigidbody.velocity = new Vector3(0, 0, 13f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Cube"))
            {
                Debug.Log("Cubes collided");
                var otherCubeController = collision.gameObject.GetComponent<CubeController>();
                if (otherCubeController != null)
                {
                    if (cubeData.number == otherCubeController.cubeData.number)
                    {
                        var vector = collision.contacts[0].point;
                        Debug.Log("Same Cubes collided");
                        // LEVEL MANAGER ICINDE MERGE METHODU OLACAK
                        // BIRLESEN KUPLER DESTROY OLCAK, YENISI OLUSACAK
                        // OLUSACAK KUP BIR KADEME USTTEN OLCAK
                    }
                }
            }
        }
    }
}

