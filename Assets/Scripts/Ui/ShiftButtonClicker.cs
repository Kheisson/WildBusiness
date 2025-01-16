using System;
using Infra;
using Player;
using Ui.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Button))]
    public class ShiftButtonClicker : MonoBehaviour
    {
        private int _initialStamina;
        private int _staminaDecreaseAmount;
        private int _spReward;

        private PlayerProfileController _playerProfileController;
        private Button _button;

        [SerializeField] private Cheque _cheque;

        public event Action OnShiftFinished;

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            SetupButton();
        }

        private void Start()
        {
            InitializeShift(5, 1);
            ShowTutorial();
        }

        private void InitializeComponents()
        {
            _button = GetComponent<Button>();
            _playerProfileController = ServiceLocator.GetService<PlayerProfileController>();
        }

        public void InitializeShift(int initialStamina, int spReward)
        {
            _initialStamina = initialStamina;
            _staminaDecreaseAmount = 1; // You can also make this configurable if needed
            _spReward = spReward;
            InitializeStamina();
            InitializeCheque();
            _button.interactable = true;
        }

        private void InitializeCheque()
        {
            _cheque.Initialize(transform as RectTransform, _initialStamina, OnChequeCollected);
        }

        private void OnChequeCollected()
        {
            InitializeShift(5, 1);
            _playerProfileController.ModifyMoney(10);
        }

        private void SetupButton()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _button.onClick.AddListener(OnButtonClick);
        }

        private void InitializeStamina()
        {
            _playerProfileController.ModifyStamina(_initialStamina, true);
        }

        private void ShowTutorial()
        {
            ServiceLocator.GetService<TutorialManager>().ShowTutorialElement(ETutorialElementsType.Clicker);
        }

        private void HideTutorial()
        {
            ServiceLocator.GetService<TutorialManager>().HideTutorialElement(ETutorialElementsType.Clicker);
        }

        private void OnButtonClick()
        {
            DecreaseStamina();
            HideTutorial();
            _cheque.ShiftButtonClicked();

            if (IsStaminaExhausted())
            {
                CompleteShift();
            }
        }

        private void DecreaseStamina()
        {
            _playerProfileController.ModifyStamina(-_staminaDecreaseAmount);
        }

        private bool IsStaminaExhausted()
        {
            return _playerProfileController.GetStamina() <= 0;
        }

        private void CompleteShift()
        {
            LlamaLog.LogInfo("No stamina left.");
            RewardPlayer();
            _button.interactable = false;
            NotifyShiftFinished();
        }

        private void RewardPlayer()
        {
            _playerProfileController.ModifySp(_spReward, true);
        }

        private void NotifyShiftFinished()
        {
            OnShiftFinished?.Invoke();
        }
    }
}
