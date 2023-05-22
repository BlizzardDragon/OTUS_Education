using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class CountdownText : MonoBehaviour, IGamePrepareListener, IInitListener
{
    public Action OnGameStarted;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private Image _background;
    private Color _backgroundStartColor;
    private int _currentTimerValue;
    private const int TIMER_VALUE = 3;
    private const float FADE_TIME = 1;
    private const int LOOPS = TIMER_VALUE;
    private const int BACKGROUND_FADE_TIME = TIMER_VALUE * 2;

    public void OnPrepareGame()
    {
        PlayCountdown();
    }

    private void PlayCountdown()
    {
        _backgroundStartColor = _background.color;
        _currentTimerValue = TIMER_VALUE;

        DOTween.Sequence()
            .Append(_countdownText.transform.DOScale(0, 0))
            .Append(_countdownText.transform.DOScale(1.5f, FADE_TIME))
            .AppendCallback(SetText)
            .SetEase(Ease.OutCirc)
            .SetLoops(LOOPS);

        DOTween.Sequence()
            .Append(_countdownText.DOFade(0, FADE_TIME))
            .SetEase(Ease.InQuint)
            .SetLoops(LOOPS);

        DOTween.Sequence()
            .Append(_background.DOFade(0, BACKGROUND_FADE_TIME))
            .AppendCallback(InvokeStartGame)
            .AppendCallback(SetStartColor);
    }

    private void SetText()
    {
        _currentTimerValue--;
        _countdownText.text = _currentTimerValue.ToString();
    }

    private void SetStartColor()
    {
        _background.color = _backgroundStartColor;
    }

    private void InvokeStartGame()
    {
        OnGameStarted?.Invoke();
    }
}
