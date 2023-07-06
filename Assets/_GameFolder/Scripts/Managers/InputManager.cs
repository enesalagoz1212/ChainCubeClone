using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Managers
{
    public class InputManager : MonoBehaviour
    {
        public float speed;
        public float maxX;
        public float minX;
        
        private float _firstTouchX;

        private void Update()
        {
            switch (GameManager.Instance.GameState)
            {
                case GameState.Start:
                    break;
                
                case GameState.ThrowAvailable:
                    if (LevelManager.Instance.CurrentCubeTransform != null)
                    {
                        HorizontalMovement();
                    }
                    break;
                
                case GameState.ThrowWaiting:
                    break;
                
                case GameState.Reset:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HorizontalMovement()
        {
            var cubeTransform = LevelManager.Instance.CurrentCubeTransform;
            if (Input.GetMouseButtonDown(0))
            {
                _firstTouchX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                float lastTouch = Input.mousePosition.x;
                float diff = lastTouch - _firstTouchX;
                
                ///// 

                var targetPosX = cubeTransform.position.x + diff * speed * Time.deltaTime;
                targetPosX = Mathf.Clamp(targetPosX, minX, maxX);

                // cubeTransform.position = new Vector3(targetPosX, cubeTransform.position.y, cubeTransform.position.z);

                var cubePos = cubeTransform.position;
                cubePos.x = targetPosX;
                cubeTransform.position = cubePos;
                
                ////// 

                _firstTouchX = lastTouch;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LevelManager.Instance.ThrowCube();
            }
            //
            
            else if(Input.GetMouseButton(0))
            {


            }
        }
    }
}

