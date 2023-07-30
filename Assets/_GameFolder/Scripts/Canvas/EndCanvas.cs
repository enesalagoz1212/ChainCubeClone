using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ChainCube.Canvases
{
    public class EndCanvas : MonoBehaviour
    {
		public RectTransform backgroundEndPanel;
		public RectTransform[] endPanelTexts;
		public RectTransform restatButton;
		public GameObject endPanel;
		public float revealDuration = 1f;

		public Button restartButton;

		private void Start()
		{
			restartButton.onClick.AddListener(OnRestartButtonClicked);
		}

		public void OnGameEnd()
		{
			restartButton.interactable = true;
			UiEndTween();
		}
		
		private void OnRestartButtonClicked()
		{
			restartButton.interactable = false;
			// RESTART BUTONUNDA CAGIRILACAK HER SEY BURADA OLSUN!
		}
		
		public void UiEndTween()
		{
			backgroundEndPanel.DOScale(Vector3.zero, revealDuration).From();		
			foreach (RectTransform endPanelText in endPanelTexts)
			{
				endPanelText.DOScale(Vector3.zero, revealDuration / 2).SetDelay(revealDuration).From();
			}
			restatButton.DOScale(Vector3.zero, revealDuration).From();
		}
    }
}