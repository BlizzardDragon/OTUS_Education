using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Architecture.Zenject.Installers;


namespace PresentationModel
{
    public class GameSystemsInstallerPM : BaseGameSystemsInstaller
    {
        protected override void InstallGameSystems()
        {
            Container.Bind<CharacterPopupPresentationModel>().AsSingle();
            Container.Bind<PresentationModel.CharacterInfo>().AsSingle();
            Container.Bind<PlayerLevel>().AsSingle();
            Container.Bind<UserInfo>().AsSingle();
        }
    }
}
