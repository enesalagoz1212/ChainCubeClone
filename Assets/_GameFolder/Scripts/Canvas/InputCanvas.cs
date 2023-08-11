using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ChainCube.Controllers;
using ChainCube.Managers;

namespace ChainCube.Canvases
{
	public class InputCanvas : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		private float _firstTouchX;

		public void OnPointerDown(PointerEventData eventData)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("Parmak dokundu: " + eventData.position);

				_firstTouchX = Input.mousePosition.x;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (Input.GetMouseButton(0))
			{
				Debug.Log("Parmak hareket ediyor: " + eventData.pointerId + " " + eventData.delta);

				var cubeTransform = LevelManager.Instance.CurrentCubeTransform;
				float lastTouch = Input.mousePosition.x;
				float diff = lastTouch - _firstTouchX;

				var targetPosX = cubeTransform.position.x + diff * GameSettingManager.Instance.gameSettings.depthSpeedZ * Time.deltaTime;
				targetPosX = Mathf.Clamp(targetPosX, GameSettingManager.Instance.gameSettings.horizontalMinX, GameSettingManager.Instance.gameSettings.horizontalMaxX);

				var cubePos = cubeTransform.position;
				cubePos.x = targetPosX;
				cubeTransform.position = cubePos;

				_firstTouchX = lastTouch;
			}
		}


		public void OnPointerUp(PointerEventData eventData)
		{
			if (Input.GetMouseButtonUp(0))
			{
				Debug.Log("Parmak kaldýrýldý: " + eventData.position);

				LevelManager.Instance.ThrowCube();
			}
		}
	}

}
