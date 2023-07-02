using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace PresentationModel
{
    public class PopUpStat : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public event Action<PopUpStat, int> OnUpdateValue;


        public void SetText(string text) => _text.text = text;
        public void DestroyPopUpStat() => Destroy(gameObject);
        public void UpdateText(int value) => OnUpdateValue?.Invoke(this, value);
    }
}
