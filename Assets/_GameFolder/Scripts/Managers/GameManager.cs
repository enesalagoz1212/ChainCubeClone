using System;
using UnityEngine;

namespace ChainCube.Managers
{
	public enum GameState
	{
		Start = 0,
		ThrowAvailable = 1,
		ThrowWaiting = 2,
		Reset = 3,
	}

	public class GameManager : MonoBehaviour
	{
		
		public static GameManager Instance { get; private set; }

		public const string RecordScorePrefsString = "RecordScore";
		public const string GameScorePrefsString = "GameScore";
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action OnCubeThrown;
		public static Action OnGameReseted;
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
		}

		private void Update()
		{
			switch (GameState)
			{
				case GameState.Start:
					break;
				case GameState.ThrowAvailable:
					break;
				case GameState.ThrowWaiting:
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
			recordScore = 0;
		}

		public void OnGameReset()
		{
			
			ChangeState(GameState.Start);
			OnGameStarted?.Invoke();  
		
		}

		
		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
		}

		public void IncreaseGameScore(int score)
		{
			gameScore += score;

			OnGameScoreIncreased?.Invoke(gameScore);
		}
		public void IncreaseRecordScore(int score)
		{
			recordScore += score;
			OnRecordScoreIncreased?.Invoke(recordScore);
		}

	}
}