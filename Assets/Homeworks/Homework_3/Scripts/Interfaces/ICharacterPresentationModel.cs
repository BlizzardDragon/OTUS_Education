using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public interface ICharacterPresentationModel
    {
        public event Action<string, string, float> OnExperienceChanged;
        public event Action OnAllowLevelUp;
        public event Action OnForbidLevelUp;


        string GetDescription();
        Sprite GetIcon();
        string GetLevel();
        string GetName();
        void OnClosedClicked();
        void OnLevelUpClicked();
        void OnShow();
    }
}
