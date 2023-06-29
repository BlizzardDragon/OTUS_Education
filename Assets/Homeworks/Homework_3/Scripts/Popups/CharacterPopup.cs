using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterPopup : MonoBehaviour
{
    [SerializeField] private GameObject _popup;

    [SerializeField] private Image _icon;

    [SerializeField] private TMP_Text _name;

    [SerializeField] private TMP_Text _level;

    [SerializeField] private TMP_Text _description;

    [SerializeField] private TMP_Text _currentExperience;
    [SerializeField] private TMP_Text _requiredExperience;
    [SerializeField] private Image _progressBarScale;
    [SerializeField] private Image _progressBarCompleted;

    [SerializeField] private Transform _statsParent;


    public void ShowPopup()
    {
        _popup.SetActive(true);
    }

    public void HidePopup()
    {
        _popup.SetActive(false);
    }

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetLevel(string level)
    {
        _level.text = level;
    }

    public void SetDescription(string description)
    {
        _description.text = description;
    }

    public void UpdateExperience(string currentExp, string requiredExp, float fillAmount)
    {
        _currentExperience.text = currentExp;
        _requiredExperience.text = requiredExp;
        _progressBarScale.fillAmount = fillAmount;
    }

    public void AllowLevelUp()
    {

    }
}
