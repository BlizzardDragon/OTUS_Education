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

        [SerializeField] private Button _closeButton;

        [SerializeField] private ButtonLevelUp _buttonLevelUp;

        [SerializeField] private PopUpStat _statPrefab;

        private Dictionary<CharacterStat, PopUpStat> _statsDictionary = new();
        private ICharacterPresentationModel _presentationModel;


        protected override void OnShow(object args)
        {
            if (args is not ICharacterPresentationModel presentationModel)
            {
                throw new Exception("Expected Presentation Model");
            }

            _presentationModel = presentationModel;

            base.OnShow(args);
            _popup.SetActive(true);

            SetLevel();
            SetIcon(_presentationModel.GetIcon());
            SetName(_presentationModel.GetName());
            SetDescription(_presentationModel.GetDescription());

            _presentationModel.PlayerLevel.OnLevelUp += SetLevel;
            _presentationModel.UserInfo.OnIconChanged += SetIcon;
            _presentationModel.UserInfo.OnNameChanged += SetName;
            _presentationModel.UserInfo.OnDescriptionChanged += SetDescription;
            _presentationModel.CharacterInfo.OnStatAdded += AddStat;
            _presentationModel.CharacterInfo.OnStatRemoved += RemoveStat;

            _presentationModel.OnExperienceChanged += UpdateExperience;
            _presentationModel.OnAllowLevelUp += AllowLevelUp;
            _presentationModel.OnForbidLevelUp += ForbidLevelUp;

            _presentationModel.OnShow(_statPrefab);

            _closeButton.onClick.AddListener(OnButtonCloseClicked);
            _buttonLevelUp.GetButton().onClick.AddListener(OnButtonLevelUpClicked);
        }

        protected override void OnHide()
        {
            base.OnHide();
            _popup.SetActive(false);

            _presentationModel.PlayerLevel.OnLevelUp -= SetLevel;
            _presentationModel.OnExperienceChanged -= UpdateExperience;
            _presentationModel.CharacterInfo.OnStatAdded -= AddStat;
            _presentationModel.CharacterInfo.OnStatRemoved -= RemoveStat;

            _closeButton.onClick.RemoveListener(OnButtonCloseClicked);
            _buttonLevelUp.GetButton().onClick.RemoveListener(OnButtonLevelUpClicked);
        }

        private void OnButtonCloseClicked()
        {
            Hide();
            _presentationModel.OnClosedClicked();
        }

        private void OnButtonLevelUpClicked()
        {
            _presentationModel.OnLevelUpClicked();
        }

        public void UpdateExperience(string text, float fillAmount)
        {
            _experience.text = text;
            _progressBarScale.fillAmount = fillAmount;
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

        private void SetLevel()
        {
            _level.text = _presentationModel.GetLevel();
        }

        private void SetDescription(string text)
        {
            _description.text = text;
        }

        private void SetName(string text)
        {
            _name.text = text;
        }

        private void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        private void AddStat(CharacterStat characterStat)
        {
            PopUpStat newPopUpStat = Instantiate(_statPrefab, _statsParent);
            _statsDictionary.Add(characterStat, newPopUpStat);
            newPopUpStat.SetText(characterStat.Name + ": " + characterStat.Value);
        }

        private void RemoveStat(CharacterStat characterStat)
        {
            PopUpStat popUpStat = _statsDictionary[characterStat];
            popUpStat.DestroyPopUpStat();
            _statsDictionary.Remove(characterStat);
        }
    }
}
