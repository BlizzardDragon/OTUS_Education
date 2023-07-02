using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace PresentationModel
{
    public class CharacterPopupObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private CharacterPopup _characterPopup;
        [SerializeField] private CharacterStatsSpawner _statsSpawner;


        public void OnStartGame()
        {
            _characterPopup.OnAddStat += _statsSpawner.AddStat;
            _characterPopup.OnRemoveStat += _statsSpawner.RemoveStat;
        }

        public void OnFinishGame()
        {
            _characterPopup.OnAddStat -= _statsSpawner.AddStat;
            _characterPopup.OnRemoveStat -= _statsSpawner.RemoveStat;
        }
    }
}
