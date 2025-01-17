using System;
using DamageNumbersPro;
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
        private const int DEFAULT_INITIAL_STAMINA = 5;
        private const int DEFAULT_SP_REWARD = 1;
        private const int MONEY_REWARD = 10;
        
        [SerializeField] private Cheque _cheque;
        [SerializeField] private DamageNumber _damageNumberGui;
        
        private int _initialStamina;
        private int _staminaDecreaseAmount;
        private int _spReward;

        private PlayerProfileController _playerProfileController;
        private TutorialManager _tutorialManager;
        private Button _button;

        public event Action OnShiftFinished;

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            SetupButton();
        }

        private void OnDisable()
        {
            CleanupButton();
        }

        private void Start()
        {
            InitializeUI();
            InitializeShift(DEFAULT_INITIAL_STAMINA, DEFAULT_SP_REWARD);
            ShowTutorial();
        }

        private void InitializeComponents()
        {
            _button = GetComponent<Button>();
            _playerProfileController = ServiceLocator.GetService<PlayerProfileController>();
            _tutorialManager = ServiceLocator.GetService<TutorialManager>();
        }

        private void InitializeUI()
        {
            _button.interactable = false;
            if (_cheque != null)
            {
                _cheque.gameObject.SetActive(false);
            }
        }

        public void InitializeShift(int initialStamina, int spReward)
        {
            _initialStamina = initialStamina;
            _staminaDecreaseAmount = 1;
            _spReward = spReward;
            
            InitializeStamina();
            InitializeCheque();
        }

        private void InitializeCheque()
        {
            if (_cheque == null)
            {
                Debug.LogError("Cheque is not assigned.");
                return;
            }

            _cheque.Initialize(transform as RectTransform, _initialStamina, OnChequeCollected);
        }

        private void OnChequeCollected()
        {
            InitializeShift(DEFAULT_INITIAL_STAMINA, DEFAULT_SP_REWARD);
            _playerProfileController.ModifyMoney(MONEY_REWARD);
            var moneyRect = _playerProfileController.GetRectOfElement(EPlayerProfileViewElement.Money);
            _damageNumberGui.SpawnGUI(moneyRect, Vector2.zero, $"+{MONEY_REWARD}");
            _button.interactable = true;
        }

        private void SetupButton()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        private void CleanupButton()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void InitializeStamina()
        {
            _playerProfileController.ModifyStamina(_initialStamina, true);
        }

        private void ShowTutorial()
        {
            _ = _tutorialManager.ShowTutorialElement(ETutorialElementsType.Clicker, EnableClicker);
        }

        private void EnableClicker()
        {
            _button.interactable = true;
            
            if (_cheque != null)
            {
                _cheque.gameObject.SetActive(true);
            }
        }

        private void HideTutorial()
        {
            _tutorialManager.HideTutorialElement(ETutorialElementsType.Clicker);
        }

        private void OnButtonClick()
        {
            DecreaseStamina();
            HideTutorial();
            _cheque.ShiftButton();

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
            _playerProfileController.ModifySp(_spReward);
            var spRect = _playerProfileController.GetRectOfElement(EPlayerProfileViewElement.Sp);
            _damageNumberGui.SpawnGUI(spRect, Vector2.zero, $"+{_spReward}");
        }

        private void NotifyShiftFinished()
        {
            OnShiftFinished?.Invoke();
        }
    }
}
