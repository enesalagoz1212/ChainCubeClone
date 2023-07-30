using UnityEngine;
using DG.Tweening;
using System;

namespace ChainCube.Canvases
{
	public class SettingsCanvas : MonoBehaviour
	{
		public GameObject settingPanel;
		public RectTransform backGroundSettinsPanel;
		public RectTransform closeButton;
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