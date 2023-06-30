using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace PresentationModel
{
    public class PopUpStat : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;


        public void SetText(string text) => _text.text = text;
    }
}
