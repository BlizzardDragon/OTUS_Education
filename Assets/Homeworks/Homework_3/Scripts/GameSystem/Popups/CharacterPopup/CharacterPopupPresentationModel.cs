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
        [SerializeField] private UserInfo _userInfo;
        [SerializeField] private PlayerLevel _playerLevel;

        private string _localizationHP = "HP";
        private string _localizationLevel = "Level";
        private const int STATS_LIMIT = 6;

        public event Action OnDescriptionChanged;
        public event Action OnIconChanged;
        public event Action OnNameChanged;
        public event Action OnLevelChanged;

        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        public event Action OnExperienceChanged;
        public event Action OnAllowLevelUp;
        public event Action OnForbidLevelUp;
        public event Action OnSpawnStat;

        [Inject]
        public void Construct(CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            _characterInfo = characterInfo;
            _playerLevel = playerLevel;
            _userInfo = userInfo;
        }

        public void SetCharacterStats(string name, Sprite icon, string description, CharacterStat[] stats)
        {
            if (stats.Length > STATS_LIMIT)
            {
                throw new Exception("Stats limit exceeded!");
            }

            foreach (var stat in stats)
            {
                _characterInfo.AddStat(stat);

                int randomValue = UnityEngine.Random.Range(1, 31);
                stat.ChangeValue(randomValue);
            }

            _userInfo.ChangeName(name);
            _userInfo.ChangeIcon(icon);
            _userInfo.ChangeDescription(description);
        }

        public void OnStart()
        {
            _userInfo.OnDescriptionChanged += CallEvent_ChangeDescription;
            _userInfo.OnIconChanged += CallEvent_ChangeIcon;
            _userInfo.OnNameChanged += CallEvent_ChangeName;
            _playerLevel.OnLevelUp += CallEvent_ChangeLevel;

            _characterInfo.OnStatAdded += CallEvent_AddStat;
            _characterInfo.OnStatRemoved += CallEvent_RemoveStat;
            
            _playerLevel.OnExperienceChanged += CallEvent_ChangedExperience;
        }

        public void OnStop()
        {
            _userInfo.OnDescriptionChanged -= CallEvent_ChangeDescription;
            _userInfo.OnIconChanged -= CallEvent_ChangeIcon;
            _userInfo.OnNameChanged -= CallEvent_ChangeName;
            _playerLevel.OnLevelUp -= CallEvent_ChangeLevel;

            _characterInfo.OnStatAdded -= CallEvent_AddStat;
            _characterInfo.OnStatRemoved -= CallEvent_RemoveStat;

            _playerLevel.OnExperienceChanged -= CallEvent_ChangedExperience;
        }

        private void CallEvent_ChangeDescription(string text) => OnDescriptionChanged?.Invoke();
        private void CallEvent_ChangeIcon(Sprite sprite) => OnIconChanged?.Invoke();
        private void CallEvent_ChangeName(string text) => OnNameChanged?.Invoke();
        private void CallEvent_ChangeLevel() => OnLevelChanged?.Invoke();

        private void CallEvent_AddStat(CharacterStat stat) => OnStatAdded?.Invoke(stat);
        private void CallEvent_RemoveStat(CharacterStat stat) => OnStatRemoved?.Invoke(stat);

        private void CallEvent_ChangedExperience(int currentExp) => OnExperienceChanged?.Invoke();

        public void CheckCanLevelUp()
        {
            if (_playerLevel.CanLevelUp())
            {
                OnAllowLevelUp?.Invoke();
            }
            else
            {
                OnForbidLevelUp?.Invoke();
            }
        }

        public float GetFillAmount()
        {
            float currentExperience = _playerLevel.CurrentExperience;
            float requiredExperience = _playerLevel.RequiredExperience;
            float fillAmount = currentExperience / requiredExperience;

            return fillAmount;
        }

        public string GetExperienceSliderText()
        {
            float currentExperience = _playerLevel.CurrentExperience;
            float requiredExperience = _playerLevel.RequiredExperience;

            return $"{_localizationHP}: {currentExperience} / {requiredExperience}";
        }

        public string GetDescription() => _userInfo.Description;
        public Sprite GetIcon() => _userInfo.Icon;
        public string GetName() => _userInfo.Name;

        public string GetLevel()
        {
            int level = _playerLevel.CurrentLevel;
            string text = $"{_localizationLevel}: {level}";
            return text;
        }

        public void OnLevelUpClicked() => _playerLevel.LevelUp();

        public void SetLocalizationHP(string text) => _localizationHP = text;
        public void SetLocalizationLevel(string text) => _localizationLevel = text;
    }
}
