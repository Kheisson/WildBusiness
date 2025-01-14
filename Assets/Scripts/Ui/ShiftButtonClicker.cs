using System;
using Infra;
using Player;
using Ui.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Button))]
    public class ShiftButtonClicker: MonoBehaviour
    {
        private const int INITIAL_STAMINA = 5;
        private const int STAMINA_COST = -1;
        private PlayerProfileController _playerProfileController;
        private Button _button;
        
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Shift);
            _playerProfileController = ServiceLocator.GetService<PlayerProfileController>();
            _playerProfileController.ModifyStamina(INITIAL_STAMINA, true);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(Shift);
        }

        private void Start()
        {
            ServiceLocator.GetService<TutorialManager>().ShowTutorialElement(ETutorialElementsType.Clicker);
        }

        private void Shift()
        {
            _playerProfileController.ModifyStamina(STAMINA_COST);
            ServiceLocator.GetService<TutorialManager>().HideTutorialElement(ETutorialElementsType.Clicker);
            
            if (_playerProfileController.GetStamina() == 0)
            {
                LlamaLog.LogInfo("No stamina left.");
                _playerProfileController.ModifySp(1, true);
                _button.interactable = false;
            }
        }
    }
}