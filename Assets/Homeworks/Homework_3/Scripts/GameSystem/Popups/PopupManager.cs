using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace PresentationModel
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private CharacterPopup _characterPopup;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _description;
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
            _presentationModel.SetCharacterStats(_name, _icon, _description, _characterStats);
        }
    }
}
