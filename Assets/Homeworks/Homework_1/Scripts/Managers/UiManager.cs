using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour, IGameFinishListener
{
    [SerializeField] private GameObject _loseScreen;
    private const float INVOKE_TIME = 3;
    

    public void OnFinishGame() => Invoke(nameof(ShowLoseScreen), INVOKE_TIME);
    private void ShowLoseScreen() => _loseScreen.SetActive(true);

}
