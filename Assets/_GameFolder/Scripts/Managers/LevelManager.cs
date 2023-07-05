using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Controllers;

namespace ChainCube.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public PlayerController playerController;
        void Start()
        {

        }

       
        void Update()
        {
            playerController.HorizontalMovement();
        }
    }
}

