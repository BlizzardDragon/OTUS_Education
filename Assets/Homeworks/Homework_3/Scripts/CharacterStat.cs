using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PresentationModel
{
    [Serializable]
    public sealed class CharacterStat
    {
        [field: SerializeField] public string Name { get; private set; }
        [ShowInInspector, ReadOnly] public int Value { get; private set; }

        public event Action<int> OnValueChanged;


        [Button]
        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}