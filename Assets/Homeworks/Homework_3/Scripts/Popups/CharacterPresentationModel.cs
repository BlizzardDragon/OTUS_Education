using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public class CharacterPresentationModel : ICharacterPresentationModel
    {
        Action<string, string, float> ICharacterPresentationModel.OnExperienceChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
    }
}
