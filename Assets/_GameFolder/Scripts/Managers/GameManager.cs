using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Managers
{
	public enum GameState
	{
		Start = 0,
		Playing = 1,
		Reset = 2,
	}

	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
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

		void Start()
		{
			GameState = GameState.Start;
		}


		void Update()
		{
			switch (GameState)
			{

				case GameState.Start:
					OnGameStart();
					break;
				case GameState.Playing:
					break;
				case GameState.Reset:
					break;
				default:
					break;
			}
		}

		private void OnGameStart()
		{
			GameState = GameState.Playing;
			OnGameStarted?.Invoke();

		}
	}

}
