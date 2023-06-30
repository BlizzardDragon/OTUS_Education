using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public interface ICharacterPresentationModel
    {
        public CharacterInfo CharacterInfo { get; set; }
        public UserInfo UserInfo { get; set; }
        public PlayerLevel PlayerLevel { get; set; }

        public event Action<string, float> OnExperienceChanged;
        public event Action OnAllowLevelUp;
        public event Action OnForbidLevelUp;
        public event Action OnSpawnStat;


        string GetDescription();
        Sprite GetIcon();
        string GetLevel();
        string GetName();
        void OnClosedClicked();
        void OnLevelUpClicked();
        void OnShow(PopUpStat statPrefab);
    }
}
