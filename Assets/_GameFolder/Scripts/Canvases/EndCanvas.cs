using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;

namespace ChainCube.Canvases
{
    public class EndCanvas : MonoBehaviour
    {
		GameSettings _gameSettings;

		public TextMeshProUGUI endScoreText;
		public TextMeshProUGUI endRecordScore;

		public RectTransform backgroundEndPanel;
		public RectTransform[] endPanelTexts;
		public RectTransform restatButton;
		public GameObject endPanel;

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
		
		public void Initialize(GameCanvas gameCanvas)
		{
			_gameSettings = gameCanvas.gameSettings;
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
			backgroundEndPanel.DOScale(Vector3.zero, _gameSettings.revealDurationTween).From();		
			foreach (RectTransform endPanelText in endPanelTexts)
			{
				endPanelText.DOScale(Vector3.zero, _gameSettings.revealDurationTween / 8).SetDelay(_gameSettings.revealDurationTween).From();
			}
			restatButton.DOScale(Vector3.zero, _gameSettings.revealDurationTween).From();
		}

		private void UpdateEndPanelScore() 
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