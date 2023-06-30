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
        [ShowInInspector] private CharacterPresentationModel _presentationModel;


        [Inject]
        public void Construct(CharacterPresentationModel characterPresentationModel)
        {
            _presentationModel = characterPresentationModel;
        }

        [Button]
        public void ShowPopup() => _characterPopup.Show(_presentationModel);
    }
}
