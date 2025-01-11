using System;
using Infra;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ShiftButtonClicker: MonoBehaviour
    {
        private PlayerProfileController _playerProfileController;
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(Shift);
            _playerProfileController = ServiceLocator.GetService<PlayerProfileController>();
        }
        
        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(Shift);
        }
        
        private void Shift()
        {
            _playerProfileController.UpdateProfile(money: _playerProfileController.PlayerProfileData.Money + 100000);
        }
    }
}