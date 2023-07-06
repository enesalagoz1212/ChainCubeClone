using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChainCube.Controllers;
using ChainCube.ScriptableObjects;

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
		public PlayerController playerController;
		public static GameManager Instance { get; private set; }
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action OnGamePlaying;
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
					OnGamePlaying?.Invoke();
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
