using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PresentationModel
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private CharacterPopup _characterPopup;
        // Пришлось изменить CharacterStat. Без этого ни как не получалось.
        [SerializeField] private CharacterStat[] _characterStats;
        [Space(15)]
        [ShowInInspector] private CharacterPopupPresentationModel _presentationModel;


        [Inject]
        public void Construct(CharacterPopupPresentationModel characterPresentationModel)
        {
            _presentationModel = characterPresentationModel;
        }

        [Button]
        public void ShowPopup()
        {
            _characterPopup.Show(_presentationModel);
            _presentationModel.SetCharacterStats(_characterStats);
        }
    }
}
