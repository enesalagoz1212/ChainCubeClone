using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using ChainCube.Managers;

namespace ChainCube.Canvases
{
    public class EndCanvas : MonoBehaviour
    {		
		public TextMeshProUGUI endScoreText; // END CANVAS
		public TextMeshProUGUI endRecordScore; // END CANVAS

		public RectTransform backgroundEndPanel;
		public RectTransform[] endPanelTexts;
		public RectTransform restatButton;
		public GameObject endPanel;
		public float revealDuration = 1f;

		public Button restartButton;

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
			GameManager.OnGameReset += OnGameReseted;		
		}
		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd -= OnGameEnd;
			GameManager.OnGameReset -= OnGameReseted;
		}

		private void Start()
		{
			restartButton.onClick.AddListener(OnRestartButtonClicked);
		}

		private void OnGameStart()
		{
			endPanel.SetActive(false);
		}
		public void OnRestartButtonClicked()
		{
			GameManager.Instance.OnGameResetAction();
		}
		
		private void OnGameReseted()
		{
		
			endPanel.SetActive(false);
			restartButton.interactable = false;
		
		}
		public void UiEndTween()
		{
			backgroundEndPanel.DOScale(Vector3.zero, revealDuration).From();		
			foreach (RectTransform endPanelText in endPanelTexts)
			{
				endPanelText.DOScale(Vector3.zero, revealDuration / 4).SetDelay(revealDuration).From();
			}
			restatButton.DOScale(Vector3.zero, revealDuration).From();
		}

		private void UpdateEndPanelScore() // END CANVAS
		{
			endScoreText.text = "Score: " + GameManager.Instance.gameScore.ToString();
			endRecordScore.text = "Record Score: " + GameManager.RecordScore.ToString();
		}

		public void OnCubeCollidedWithReset()
		{
			endPanel.SetActive(true);
			UpdateEndPanelScore();
		}
		private void OnGameEnd()
		{
			restartButton.interactable = true;
			OnCubeCollidedWithReset();
			UiEndTween();
		}
	}
}