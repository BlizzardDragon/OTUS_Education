using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour, IGamePrepareListener
{
    [SerializeField] TextMeshProUGUI _countdownText;
    
    public void OnPrepareGame()
    {
        throw new System.NotImplementedException();
    }

}
