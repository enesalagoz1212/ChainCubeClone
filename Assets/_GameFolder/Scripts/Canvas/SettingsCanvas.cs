using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ChainCube.Managers;
using ChainCube.ScriptableObjects;

namespace ChainCube.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{
		public GameObject settingPanel;
		public RectTransform backGroundSettinsPanel, closeButton;
		public RectTransform vibrationButton, soundButton, musicButton;
		float revealDurationTween = 0.5f;


		public void UiSettingsTween()
		{
			backGroundSettinsPanel.DOScale(Vector3.zero, revealDurationTween).From();
			closeButton.DOScale(Vector3.zero,revealDurationTween).SetDelay(revealDurationTween).From();
			vibrationButton.DOScale(Vector3.zero, revealDurationTween).From();
			musicButton.DOScale(Vector3.zero, revealDurationTween).From();
			soundButton.DOScale(Vector3.zero, revealDurationTween).From();
		}

	}
}

