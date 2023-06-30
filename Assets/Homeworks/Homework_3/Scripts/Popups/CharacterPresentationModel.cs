using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PresentationModel
{
    public class CharacterPresentationModel : ICharacterPresentationModel
    {
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private PlayerLevel _playerLevel;
        [SerializeField] private UserInfo _userInfo;

        Action<string, string, float> ICharacterPresentationModel.OnExperienceChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        [Inject]
        public void Construct(CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            _characterInfo = characterInfo;
            _playerLevel = playerLevel;
            _userInfo = userInfo;
        }

        public string GetDescription()
        {
            throw new System.NotImplementedException();
        }

        public Sprite GetIcon()
        {
            throw new System.NotImplementedException();
        }

        public string GetLevel()
        {
            throw new System.NotImplementedException();
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }

        public void OnClosedClicked()
        {
            throw new NotImplementedException();
        }

        public void OnLevelUpClicked()
        {
            throw new NotImplementedException();
        }
    }
}
