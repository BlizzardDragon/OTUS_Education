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
    public Action OnGameResumed;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _pauseButton;
    private Color _backgroundStartColor;
    private Color _textStartColor;
    private int _countdownValue;
    private bool _isFirstStart = true;
    private const int TIMER_VALUE = 3;
    private const float FADE_TIME = 1;
    private const int LOOPS = TIMER_VALUE;
    private const int BACKGROUND_FADE_TIME = TIMER_VALUE;


    public void OnPrepareGame() => PlayCountdown();

    private void PlayCountdown()
    {
        _backgroundStartColor = _background.color;
        _textStartColor = _countdownText.color;
        _background.enabled = true;
        _countdownText.enabled = true;
        _countdownValue = TIMER_VALUE;

        DOTween.Sequence()
            .AppendCallback(SetText)
            .Append(_countdownText.transform.DOScale(0, 0))
            .Append(_countdownText.transform.DOScale(1.5f, FADE_TIME))
            .SetEase(Ease.OutCirc)
            .SetLoops(LOOPS);

        DOTween.Sequence()
            .Append(_countdownText.DOFade(0, FADE_TIME))
            .SetEase(Ease.InQuint)
            .SetLoops(LOOPS);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(_background.DOFade(0, BACKGROUND_FADE_TIME))
            .AppendCallback(InvokePlayGame)
            .AppendCallback(ShowGameScreen);
    }

    private void SetText()
    {
        _countdownText.text = _countdownValue.ToString();
        _countdownValue--;
    }

    private void ShowGameScreen()
    {
        _background.enabled = false;
        _background.color = _backgroundStartColor;
        _countdownText.color = _textStartColor;
        _countdownText.enabled = false;
        _pauseButton.SetActive(true);
    }

    private void InvokePlayGame()
    {
        if (_isFirstStart)
        {
            OnGameStarted?.Invoke();
            _isFirstStart = false;
        }
        else
        {
            OnGameResumed?.Invoke();
        }
    }
}
