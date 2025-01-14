using Ui;

namespace Player
{
    public class PlayerProfileController
    {
        private readonly PlayerProfileView _playerProfileView;
        private readonly PlayerProfileSo _playerProfileData;
        
        public PlayerProfileController(PlayerProfileSo playerProfileData, PlayerProfileView playerProfileView)
        {
            _playerProfileData = playerProfileData;
            _playerProfileView = playerProfileView;
            
            _playerProfileData.OnProfileUpdated -= UpdateView;
            _playerProfileData.OnProfileUpdated += UpdateView;
            UpdateView();
        }

        private void UpdateView()
        {
            _playerProfileView.SetAvatar(_playerProfileData.Avatar);
            _playerProfileView.SetTitle(_playerProfileData.Title);
            _playerProfileView.SetNickname(_playerProfileData.Nickname);
            _playerProfileView.SetXp(_playerProfileData.Xp);
            _playerProfileView.SetSp(_playerProfileData.Sp);
            _playerProfileView.SetMoney(_playerProfileData.Money);
            _playerProfileView.SetDiamonds(_playerProfileData.Diamonds);
            _playerProfileView.SetTickets(_playerProfileData.Tickets);
        }
        
        public bool ModifyStamina(int delta, bool set = false)
        {
            return _playerProfileData.ModifyStamina(delta, set);
        }
        
        public int GetStamina()
        {
            return _playerProfileData.Stamina;
        }
        
        public bool ModifySp(int delta, bool set = false)
        {
            return _playerProfileData.ModifySp(delta, set);
        }
        
        public int GetSp()
        {
            return _playerProfileData.Sp;
        }
        
        public bool ModifyMoney(int delta, bool set = false)
        {
            return _playerProfileData.ModifyMoney(delta, set);
        }
        
        public int GetMoney()
        {
            return _playerProfileData.Money;
        }
        
        public bool ModifyDiamonds(int delta, bool set = false)
        {
            return _playerProfileData.ModifyDiamonds(delta, set);
        }
        
        public int GetDiamonds()
        {
            return _playerProfileData.Diamonds;
        }
        
        public bool ModifyTickets(int delta, bool set = false)
        {
            return _playerProfileData.ModifyTickets(delta, set);
        }
        
        public int GetTickets()
        {
            return _playerProfileData.Tickets;
        }
        
        public bool ModifyXp(int delta, bool set = false)
        {
            return _playerProfileData.ModifyXp(delta, set);
        }
        
        public int GetXp()
        {
            return _playerProfileData.Xp;
        }
        
        public void UpdateAvatar(UnityEngine.Sprite newAvatar)
        {
            _playerProfileData.UpdateAvatar(newAvatar);
        }
        
        public void UpdateTitle(string newTitle)
        {
            _playerProfileData.UpdateTitle(newTitle);
        }
        
        public void UpdateNickname(string newNickname)
        {
            _playerProfileData.UpdateNickname(newNickname);
        }
    }
}