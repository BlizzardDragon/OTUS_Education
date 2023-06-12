namespace FrameworkUnity.Interfaces.Listeners.GameListeners
{
    public interface IGameListener { }

    public interface IGamePrepareListener : IGameListener
    {
        void OnPrepareGame();
    }

    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}