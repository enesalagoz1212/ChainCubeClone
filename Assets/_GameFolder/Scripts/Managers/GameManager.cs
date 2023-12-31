using System;
using UnityEngine;
using DG.Tweening;
using ChainCube.Pooling;

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
		
		#region Variables

		public static GameManager Instance { get; private set; }

		public const string RecordScorePrefsString = "RecordScore";
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action OnGameEnd;
		public static Action OnGameReset;
		public static Action<int> OnGameScoreIncreased;
		public static Action<int> OnRecordScoreTexted;
		public static Action<int> OnBombCubeCountChanged;
		public static Action<int> OnColoredCubeCountChanged;

		[SerializeField] private LevelManager levelManager;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private InputManager inputManager;
		[SerializeField] private ParticlePool particlePool;

		public int gameScore;

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

		#endregion
		
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
			GameInitialize();
		}

		private void GameInitialize()
		{
			levelManager.Initialize(uiManager, inputManager,particlePool);
			uiManager.Initialize(levelManager, inputManager);
			inputManager.Initialize();
			
			OnGameStart();
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
			});
		}

		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
		}

		public void IncreaseGameScore(int score)
		{
			gameScore += score;

			if (gameScore >= RecordScore)
			{
				RecordScore = gameScore;
				OnRecordScoreTexted?.Invoke(RecordScore);
			}
			OnGameScoreIncreased?.Invoke(gameScore);
		}
		
		public void DecreaseColoredCount(int count)
		{
			GameSettingManager.ColoredCount--;
			OnColoredCubeCountChanged?.Invoke(GameSettingManager.ColoredCount);
		}
		
		public void DecreaseBombCount(int count)
		{
			GameSettingManager.BombCount--;
		    OnBombCubeCountChanged?.Invoke(GameSettingManager.BombCount);
		}
	}
}