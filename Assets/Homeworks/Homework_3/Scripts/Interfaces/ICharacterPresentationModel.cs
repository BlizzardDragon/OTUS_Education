using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public interface ICharacterPresentationModel
    {
        public Action<string, string, float> OnExperienceChanged { get; internal set; }


        string GetDescription();
        Sprite GetIcon();
        string GetLevel();
        string GetName();
    }
}
