using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace PresentationModel
{
    public class CharacterPopup : Popup
    {
        [SerializeField] private GameObject _popup;

        [SerializeField] private Image _icon;

        [SerializeField] private TMP_Text _name;

        [SerializeField] private TMP_Text _level;

        [SerializeField] private TMP_Text _description;

        [SerializeField] private TMP_Text _experience;
        [SerializeField] private Image _progressBarScale;
        [SerializeField] private Image _progressBarCompleted;

        [SerializeField] private Transform _statsParent;

        [SerializeField] private ButtonLevelUp _buttonLevelUp;
        
        [SerializeField] private Button _closeButton;


        private ICharacterPresentationModel _presentationModel;

        private string _localizationHP = "HP";


        protected override void OnShow(object args)
        {
            if (args is not ICharacterPresentationModel presentationModel)
            {
                throw new Exception("Expected Presentation Model");
            }

            _presentationModel = presentationModel;

            base.OnShow(args);
            _popup.SetActive(true);
            _icon.sprite = _presentationModel.GetIcon();
            _name.text = _presentationModel.GetName();
            _level.text = _presentationModel.GetLevel();
            _description.text = _presentationModel.GetDescription();
            _presentationModel.OnExperienceChanged += UpdateExperience;
            _closeButton.onClick.AddListener(OnButtonCloseClicked);
            _buttonLevelUp.GetButton().onClick.AddListener(OnButtonLevelUpClicked);
        }

        protected override void OnHide()
        {
            base.OnHide();
            _popup.SetActive(false);
            _presentationModel.OnExperienceChanged -= UpdateExperience;
            _closeButton.onClick.RemoveListener(OnButtonCloseClicked);
            _buttonLevelUp.GetButton().onClick.RemoveListener(OnButtonLevelUpClicked);
        }

        private void OnButtonCloseClicked()
        {
            _presentationModel.OnClosedClicked();
        }

        private void OnButtonLevelUpClicked()
        {
            _presentationModel.OnLevelUpClicked();
        }

        public void UpdateExperience(string currentExp, string requiredExp, float fillAmount)
        {
            _experience.text = $"{_localizationHP}: {currentExp} / {requiredExp}";
            _progressBarScale.fillAmount = fillAmount;

            if (fillAmount < 1)
            {
                ForbidLevelUp();
            }
            else
            {
                AllowLevelUp();
            }
        }

        public void AllowLevelUp()
        {
            _progressBarCompleted.enabled = true;
            _buttonLevelUp.ActivateButton();
        }

        public void ForbidLevelUp()
        {
            _progressBarCompleted.enabled = false;
            _buttonLevelUp.DeactivateButton();
        }

        public void SetLocalizationHP(string text)
        {
            _localizationHP = text;
        }

        // public void SetIcon(Sprite sprite)
        // {
        //     _icon.sprite = sprite;
        // }

        // public void SetName(string name)
        // {
        //     _name.text = name;
        // }

        // public void SetLevel(string level)
        // {
        //     _level.text = level;
        // }

        // public void SetDescription(string description)
        // {
        //     _description.text = description;
        // }
    }
}
