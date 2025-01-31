using Infra;
using Infra.Events;
using Player;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ShiftButtonClicker : MonoBehaviour
    {
        private const int DEFAULT_INITIAL_STAMINA = 5;
        private const int DEFAULT_SP_REWARD = 1;

        [SerializeField] private Cheque _cheque;
        [SerializeField] private TutorialHandler _tutorialHandler;
        
        private PlayerProfileController _playerProfileController;
        private Button _button;
        private int _staminaDecreaseAmount;
        private int _spReward;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _playerProfileController = ServiceLocator.GetService<PlayerProfileController>();
            EventManager.AddListener<ChequeCollectedEventArgs>(OnChequeCollected);

            if (_cheque == null || _tutorialHandler == null || _playerProfileController == null)
                Debug.LogError("Missing component references in ShiftButtonClicker.");
        }

        private void Start()
        {
            InitializeShift(DEFAULT_SP_REWARD);
            SetupButton();
            ShowTutorial();
        }

        private void SetupButton()
        {
            _button.onClick.AddListener(OnButtonClick);
            _button.interactable = false;
        }

        private void InitializeShift(int spReward)
        {
            _playerProfileController.ModifyStamina(DEFAULT_INITIAL_STAMINA, true);
            _staminaDecreaseAmount = 1;
            _spReward = spReward;
            
            SetupCheque();
            EnableClicker();
        }

        private void SetupCheque()
        {
            if (_cheque == null) return;
            
            _cheque.gameObject.SetActive(true);
            _cheque.Initialize(transform as RectTransform, DEFAULT_INITIAL_STAMINA, _tutorialHandler);
        }

        private void ShowTutorial()
        {
            _tutorialHandler.ShowTutorial(ETutorialElementsType.Clicker, EnableClicker);
        }

        private void EnableClicker()
        {
            _button.interactable = true;
        }

        private void HideTutorial()
        {
            _tutorialHandler.HideTutorial(ETutorialElementsType.Clicker);
        }

        private void OnButtonClick()
        {
            _playerProfileController.ModifyStamina(-_staminaDecreaseAmount);
            HideTutorial();
            _cheque.ShiftButton();

            if (IsStaminaExhausted())
            {
                CompleteShift();
            }
        }

        private bool IsStaminaExhausted()
        {
            return _playerProfileController.GetStamina() <= 0;
        }

        private void CompleteShift()
        {
            _playerProfileController.ModifySp(_spReward);
            _button.interactable = false;
            EventManager.TriggerEvent(new ShiftCompletedEventArgs(DEFAULT_SP_REWARD));
        }
        
        private void OnChequeCollected(ChequeCollectedEventArgs args)
        {
            _playerProfileController.ModifyMoney(args.ChequeValue);
            InitializeShift(DEFAULT_SP_REWARD);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
