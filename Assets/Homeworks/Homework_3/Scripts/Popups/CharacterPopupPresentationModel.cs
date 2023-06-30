using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PresentationModel
{
    public class CharacterPopupPresentationModel : ICharacterPresentationModel
    {
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private PlayerLevel _playerLevel;
        [SerializeField] private UserInfo _userInfo;

        public event Action<string, string, float> OnExperienceChanged;
        public event Action OnAllowLevelUp;
        public event Action OnForbidLevelUp;


        [Inject]
        public void Construct(CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            _characterInfo = characterInfo;
            _playerLevel = playerLevel;
            _userInfo = userInfo;
        }

        public void OnShow()
        {
            UpdatePopupExperience();
        }

        private void UpdatePopupExperience()
        {
            float currentExperience = _playerLevel.CurrentExperience;
            float requiredExperience = _playerLevel.RequiredExperience;

            string currentExpText = currentExperience.ToString();
            string requiredExpText = requiredExperience.ToString();
            float fillAmount = currentExperience / requiredExperience;

            OnExperienceChanged?.Invoke(currentExpText, requiredExpText, fillAmount);
            CheckLevelUp(fillAmount);
        }

        private void CheckLevelUp(float fillAmount)
        {
            if (fillAmount < 1)
            {
                OnForbidLevelUp?.Invoke();
            }
            else
            {
                OnAllowLevelUp?.Invoke();
            }
        }

        public string GetDescription()
        {
            return _userInfo.Description;
        }

        public Sprite GetIcon()
        {
            return _userInfo.Icon;
        }

        public string GetLevel()
        {
            return _playerLevel.CurrentLevel.ToString();
        }

        public string GetName()
        {
            return _userInfo.Name;
        }

        public void OnLevelUpClicked()
        {
            _playerLevel.LevelUp();
        }

        public void OnClosedClicked() { }
    }
}
