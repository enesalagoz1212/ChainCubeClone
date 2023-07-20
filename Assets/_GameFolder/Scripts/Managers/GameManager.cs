using System;
using UnityEngine;
using DG.Tweening;

namespace ChainCube.Managers
{
	public enum GameState
	{
		Start = 0,
		ThrowAvailable = 1,
		ThrowWaiting = 2,
		GameEnd = 3,
		Reset = 4,
	}

	public class GameManager : MonoBehaviour
	{

		public static GameManager Instance { get; private set; }

		public const string RecordScorePrefsString = "RecordScore";
		public const string GameScorePrefsString = "GameScore";
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action OnCubeThrown;
		public static Action OnGameEnd;
		public static Action OnGameReset;
		public static Action<int> OnGameScoreIncreased;
		public static Action<int> OnRecordScoreIncreased;

		public int gameScore;
		public int recordScore;

		public static int GameScore
		{
			get
			{
				return PlayerPrefs.GetInt(GameScorePrefsString);
			}
			set
			{
				PlayerPrefs.SetInt(GameScorePrefsString, value);
			}
		}

		public static int RecordScore
		{
			get
			{
				return PlayerPrefs.GetInt(RecordScorePrefsString);
			}
			set
			{
				PlayerPrefs.SetInt(RecordScorePrefsString, value);
			}
		}


		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}

		private void Start()
		{

			OnGameStart();
			recordScore = PlayerPrefs.GetInt(RecordScorePrefsString);
		}

		private void Update()
		{
			switch (GameState)
			{
				case GameState.Start:

					break;
				case GameState.ThrowAvailable:
					if (Input.GetKeyDown(KeyCode.F))
					{
						// Game Failed!
						EndGame();
					}
					break;
				case GameState.ThrowWaiting:
					break;
				case GameState.GameEnd:
					break;
				case GameState.Reset:
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnGameStart()
		{
			GameState = GameState.Start;
			OnGameStarted?.Invoke();
			GameState = GameState.ThrowAvailable;
			gameScore = 0; 
		}

		public void EndGame()
		{
			ChangeState(GameState.GameEnd);
			OnGameEnd?.Invoke();
		}

		public void OnGameResetAction()
		{
			ChangeState(GameState.Reset);
			OnGameReset?.Invoke();

			DOVirtual.DelayedCall(0.2f, () =>
			{
				OnGameStart();
				UIManager.Instance.endPanel.gameObject.SetActive(false);
			});
		}

		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
		}

		public void IncreaseGameScore(int score)
		{
			gameScore += score;

			if (gameScore > recordScore)
			{
				recordScore = gameScore;
				UIManager.Instance.UpdateScoreText();
			}

			OnGameScoreIncreased?.Invoke(gameScore);
		}
		
		public void IncreaseRecordScore(int score)
		{
			score = 0;
			recordScore += score;
			PlayerPrefs.SetInt(RecordScorePrefsString, recordScore);
			OnRecordScoreIncreased?.Invoke(recordScore);
		}
		
		public void RestartGame()
		{
			OnGameResetAction();
		}
		
		public void QuitGame()
		{
			Application.Quit();
		}
		
	}
}