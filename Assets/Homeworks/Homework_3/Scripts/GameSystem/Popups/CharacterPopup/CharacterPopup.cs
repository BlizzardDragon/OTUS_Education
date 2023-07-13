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

        private ICharacterPresentationModel _presentationModel;

        public event Action<CharacterStat, Transform> OnAddStat;
        public event Action<CharacterStat> OnRemoveStat;


        protected override void OnShow(object args)
        {
            if (args is not ICharacterPresentationModel presentationModel)
            {
                throw new Exception("Expected Presentation Model");
            }

            _presentationModel = presentationModel;

            base.OnShow(args);
            _popup.SetActive(true);

            _presentationModel.OnLevelChanged += SetLevel;
            _presentationModel.OnIconChanged += SetIcon;
            _presentationModel.OnNameChanged += SetName;
            _presentationModel.OnDescriptionChanged += SetDescription;

            _presentationModel.OnExperienceChanged += UpdateExperience;
            _presentationModel.OnExperienceChanged += UpdateProgressBar;
            _presentationModel.OnExperienceChanged += CheckCanLevelUp;

            _presentationModel.OnAllowLevelUp += AllowLevelUp;
            _presentationModel.OnForbidLevelUp += ForbidLevelUp;

            _presentationModel.OnStatAdded += AddStat;
            _presentationModel.OnStatRemoved += RemoveStat;

            _closeButton.onClick.AddListener(OnButtonCloseClicked);
            _buttonLevelUp.GetButton().onClick.AddListener(OnButtonLevelUpClicked);

            _presentationModel.OnStart();
            UpdateStats();
        }

        protected override void OnHide()
        {
            base.OnHide();
            _popup.SetActive(false);

            _presentationModel.OnLevelChanged -= SetLevel;
            _presentationModel.OnIconChanged -= SetIcon;
            _presentationModel.OnNameChanged -= SetName;
            _presentationModel.OnDescriptionChanged -= SetDescription;

            _presentationModel.OnExperienceChanged -= UpdateExperience;
            _presentationModel.OnExperienceChanged -= UpdateProgressBar;
            _presentationModel.OnExperienceChanged -= CheckCanLevelUp;

            _presentationModel.OnAllowLevelUp -= AllowLevelUp;
            _presentationModel.OnForbidLevelUp -= ForbidLevelUp;

            _presentationModel.OnStatAdded -= AddStat;
            _presentationModel.OnStatRemoved -= RemoveStat;

            _closeButton.onClick.RemoveListener(OnButtonCloseClicked);
            _buttonLevelUp.GetButton().onClick.RemoveListener(OnButtonLevelUpClicked);
        }

        private void UpdateStats()
        {
            SetLevel();
            SetDescription();
            SetName();
            SetIcon();
            UpdateExperience();
            UpdateProgressBar();
            CheckCanLevelUp();
        }

        private void SetLevel() => _level.text = _presentationModel.GetLevel();
        private void SetDescription() => _description.text = _presentationModel.GetDescription();
        private void SetName() => _name.text = _presentationModel.GetName();
        private void SetIcon() => _icon.sprite = _presentationModel.GetIcon();

        private void UpdateExperience() => _experience.text = _presentationModel.GetExperienceSliderText();
        private void UpdateProgressBar() => _progressBarScale.fillAmount = _presentationModel.GetFillAmount();
        private void CheckCanLevelUp() => _presentationModel.CheckCanLevelUp();

        private void AllowLevelUp()
        {
            _progressBarCompleted.enabled = true;
            _buttonLevelUp.ActivateButton();
        }

        private void ForbidLevelUp()
        {
            _progressBarCompleted.enabled = false;
            _buttonLevelUp.DeactivateButton();
        }

        private void AddStat(CharacterStat characterStat) => OnAddStat?.Invoke(characterStat, _statsParent);
        private void RemoveStat(CharacterStat characterStat) => OnRemoveStat?.Invoke(characterStat);

        private void OnButtonCloseClicked()
        {
            Hide();
            _presentationModel.OnStop();
        }

        private void OnButtonLevelUpClicked() => _presentationModel.OnLevelUpClicked();
    }
}
