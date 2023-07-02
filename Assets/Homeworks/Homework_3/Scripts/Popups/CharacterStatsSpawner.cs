using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace PresentationModel
{
    public class CharacterStatsSpawner : MonoBehaviour
    {
        [SerializeField] private PopUpStat _statPrefab;

        private Dictionary<CharacterStat, PopUpStat> _statsDictionary = new();


        public void AddStat(CharacterStat characterStat, Transform statsParent)
        {
            PopUpStat newPopUpStat = Instantiate(_statPrefab, statsParent);
            _statsDictionary.Add(characterStat, newPopUpStat);
            UpdateTextToPopUpStat(newPopUpStat, characterStat.Value);

            characterStat.OnValueChanged += newPopUpStat.UpdateText;
            newPopUpStat.OnUpdateValue += UpdateTextToPopUpStat;
        }

        public void RemoveStat(CharacterStat characterStat)
        {
            PopUpStat popUpStat = _statsDictionary[characterStat];
            popUpStat.DestroyPopUpStat();
            _statsDictionary.Remove(characterStat);

            characterStat.OnValueChanged -= popUpStat.UpdateText;
            popUpStat.OnUpdateValue -= UpdateTextToPopUpStat;
        }

        private void UpdateTextToPopUpStat(PopUpStat popUpStat, int value)
        {
            CharacterStat characterStat = GetKeyByValue(_statsDictionary, popUpStat);
            popUpStat.SetText(characterStat.Name + ": " + characterStat.Value);
        }

        private CharacterStat GetKeyByValue<CharacterStat, PopUpStat>
            (Dictionary<CharacterStat, PopUpStat> dictionary, PopUpStat value)
        {
            var key = dictionary.FirstOrDefault(x => x.Value.Equals(value)).Key;
            if (key == null)
            {
                throw new KeyNotFoundException($"The value '{value}' was not found in the dictionary.");
            }
            return key;
        }
    }
}
