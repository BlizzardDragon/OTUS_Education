using UnityEngine;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.GameManagers;

// Готово.
namespace ShootEmUp
{
    public class CharacterEmptyHPObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private GameManager _gameManager;
        private Character _character;


        [Inject]
        public void Construct(Character character, GameManager gameManager)
        {
            _character = character;
            _gameManager = gameManager;
        }

        public void OnStartGame() => _character.HitPointsComponent.OnEmptyHP += OnCharacterDeath;
        public void OnFinishGame() => _character.HitPointsComponent.OnEmptyHP -= OnCharacterDeath;

        private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();
    }
}
