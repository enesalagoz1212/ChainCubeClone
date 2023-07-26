using UnityEngine;
using DG.Tweening;

namespace ChainCube.Managers
{
	public class TweenManagerUI : MonoBehaviour
	{
		public RectTransform[] downYTransforms;
		public RectTransform backgroundEndPanel;
		public RectTransform[] endPanelTexts;
		public RectTransform backGroundSettinsPanel, closeButton;
		public RectTransform vibrationButton, soundButton, musicButton;
		public float revealDuration;

		public void UiEndTween()
		{
			backgroundEndPanel.DOScale(Vector3.zero, revealDuration).From();
			foreach (RectTransform downYTransform in downYTransforms)
			{
				downYTransform.DOAnchorPosY(-1300f, revealDuration).From();
			}
			foreach (RectTransform endPanelText in endPanelTexts)
			{
				endPanelText.DOScale(Vector3.zero, revealDuration / 2).SetDelay(revealDuration).From();
			}

		}
		public void OnClickTween(RectTransform rectTransform)
		{
			rectTransform.DOScale(rectTransform.localScale * 1.25f, revealDuration).SetLoops(2, LoopType.Yoyo);
		}
		public void UiSettingsTween()
		{
			backGroundSettinsPanel.DOScale(Vector3.zero, revealDuration).From();
			closeButton.DOScale(Vector3.zero, revealDuration).SetDelay(revealDuration).From();

			vibrationButton.DOAnchorPosY(610f, revealDuration).SetDelay(revealDuration).From();
			musicButton.DOAnchorPosY(-1572f, revealDuration).SetDelay(revealDuration).From();
			soundButton.DOAnchorPosX(-1000f, revealDuration).SetDelay(revealDuration).From();
		}
	}
}

