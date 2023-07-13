using FrameworkUnity.Architecture.Zenject.Installers;

public class BootstrapInstallerPM : BaseBootstrapInstaller
{
    protected override void Start()
    {
        base.Start();
        _gameManager.StartGame();
    }
}
