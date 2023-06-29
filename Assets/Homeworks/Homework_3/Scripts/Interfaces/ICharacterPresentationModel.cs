using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PresentationModel
{
    public class ICharacterPresentationModel : MonoBehaviour
    {
        internal string Name => _name;
        private string _name;

        public Action<string, string, float> OnExperienceChanged { get; internal set; }


        internal string GetDescription()
        {
            throw new NotImplementedException();
        }

        internal Sprite GetIcon()
        {
            throw new NotImplementedException();
        }

        internal string GetLevel()
        {
            throw new NotImplementedException();
        }

        internal string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
