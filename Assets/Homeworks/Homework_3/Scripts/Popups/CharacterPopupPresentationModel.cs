using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PresentationModel
{
    public class CharacterPopupPresentationModel : ICharacterPresentationModel
    {
        [field: SerializeField] public CharacterInfo CharacterInfo { get; set; }
        [field: SerializeField] public UserInfo UserInfo { get; set; }
        [field: SerializeField] public PlayerLevel PlayerLevel { get; set; }

        private PopUpStat _statPrefab;

        private string _localizationHP = "HP";
        private string _localizationLevel = "Level";

        public event Action<string, float> OnExperienceChanged;
        public event Action OnAllowLevelUp;
        public event Action OnForbidLevelUp;


        [Inject]
        public void Construct(CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            CharacterInfo = characterInfo;
            PlayerLevel = playerLevel;
            UserInfo = userInfo;
        }

        public void SetCharacterStats(CharacterStat[] stats)
        {
            foreach (var stat in stats)
            {
                CharacterInfo.AddStat(stat);
            }
        }

        public void OnShow()
        {
            UpdatePopupExperience(PlayerLevel.CurrentExperience);
            PlayerLevel.OnExperienceChanged += UpdatePopupExperience;
        }

        public void OnClosedClicked()
        {
            PlayerLevel.OnExperienceChanged -= UpdatePopupExperience;
        }

        private void UpdatePopupExperience(int currentExp)
        {
            float currentExperience = currentExp;
            float requiredExperience = PlayerLevel.RequiredExperience;

            string text = $"{_localizationHP}: {currentExperience} / {requiredExperience}";
            float fillAmount = currentExperience / requiredExperience;

            OnExperienceChanged?.Invoke(text, fillAmount);

            CheckExperienceLimit(fillAmount);
        }

        private void CheckExperienceLimit(float fillAmount)
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
            return UserInfo.Description;
        }

        public Sprite GetIcon()
        {
            return UserInfo.Icon;
        }

        public string GetLevel()
        {
            UpdatePopupExperience(PlayerLevel.CurrentExperience);

            int level = PlayerLevel.CurrentLevel;
            string text = $"{_localizationLevel}: {level}";
            return text;
        }

        public string GetName()
        {
            return UserInfo.Name;
        }

        public void OnLevelUpClicked()
        {
            PlayerLevel.LevelUp();
        }

        public void SetLocalizationHP(string text)
        {
            _localizationHP = text;
        }

        public void SetLocalizationLevel(string text)
        {
            _localizationLevel = text;
        }
    }
}
