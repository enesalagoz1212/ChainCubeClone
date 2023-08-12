using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ChainCube.Controllers;
using ChainCube.Managers;
using DG.Tweening;
using UnityEngine.UI;

namespace ChainCube.Canvases
{
	public class InputCanvas : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		private InputManager _inputManager;

		public void Initialize(InputManager inputManager)
		{
			_inputManager = inputManager;
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			_inputManager.OnScreenTouch(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			_inputManager.OnScreenDrag(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			_inputManager.OnScreenUp(eventData);
		}
	}
}