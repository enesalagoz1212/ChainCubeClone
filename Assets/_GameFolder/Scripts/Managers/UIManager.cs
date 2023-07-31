using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using ChainCube.Canvases;

namespace ChainCube.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public GameCanvas gameCanvas;
        public EndCanvas endCanvas;
		
		private void Awake()
		{
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
 
    }
}

