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
        [Space(15)]
        [ShowInInspector] private CharacterPopupPresentationModel _presentationModel;


        [Inject]
        public void Construct(CharacterPopupPresentationModel characterPresentationModel)
        {
            _presentationModel = characterPresentationModel;
        }

        [Button]
        public void ShowPopup() => _characterPopup.Show(_presentationModel);
    }
}
