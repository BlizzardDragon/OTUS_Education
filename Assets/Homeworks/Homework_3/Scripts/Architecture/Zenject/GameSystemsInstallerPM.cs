using FrameworkUnity.Architecture.Zenject.Installers;


namespace PresentationModel
{
    public class GameSystemsInstallerPM : BaseGameSystemsInstaller
    {
        protected override void InstallGameSystems()
        {
            Container.Bind<CharacterPopupPresentationModel>().AsTransient();
            Container.Bind<CharacterInfo>().AsSingle();
            Container.Bind<PlayerLevel>().AsSingle();
            Container.Bind<UserInfo>().AsSingle();
        }
    }
}
