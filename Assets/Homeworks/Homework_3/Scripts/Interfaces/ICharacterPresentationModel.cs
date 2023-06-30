using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public interface ICharacterPresentationModel
    {
        public event Action<string, string, float> OnExperienceChanged;


        string GetDescription();
        Sprite GetIcon();
        string GetLevel();
        string GetName();
        void OnClosedClicked();
        void OnLevelUpClicked();
        void OnShow();
    }
}
