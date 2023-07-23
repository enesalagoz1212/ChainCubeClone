using ChainCube.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Managers
{
    public class GameSettingManager : MonoBehaviour
    {
        public static GameSettingManager Instance { get; private set; }
        public GameSettings gameSettings;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}

