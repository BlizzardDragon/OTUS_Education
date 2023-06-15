using FrameworkUnity.Architecture.Installers;


namespace ShootEmUp
{
    public class BootstrapInstaller : BaseBootstrapInstaller
    {
        protected override void Start()
        {
            base.Start();
            _gameManager.StartGame();
        }
    }
}
