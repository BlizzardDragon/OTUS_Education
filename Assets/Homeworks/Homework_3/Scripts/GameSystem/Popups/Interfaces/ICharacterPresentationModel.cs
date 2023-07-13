using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public interface ICharacterPresentationModel
    {
        public event Action OnDescriptionChanged;
        public event Action OnIconChanged;
        public event Action OnNameChanged;
        public event Action OnLevelUp;

        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;


        public event Action OnExperienceChanged;
        public event Action OnAllowLevelUp;
        public event Action OnForbidLevelUp;
        public event Action OnSpawnStat;


        string GetDescription();
        Sprite GetIcon();
        string GetLevel();
        string GetName();
        void OnStop();
        void OnLevelUpClicked();
        void OnStart();
        string GetExperienceSliderText();
        float GetFillAmount();
        void CheckCanLevelUp();
    }
}
