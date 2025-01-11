using Ui;

namespace Player
{
    public class PlayerProfileController
    {
        private readonly PlayerProfileView _playerProfileView;
        
        public PlayerProfileSo PlayerProfileData { get; }

        public PlayerProfileController(PlayerProfileSo playerProfileData, PlayerProfileView playerProfileView)
        {
            PlayerProfileData = playerProfileData;
            _playerProfileView = playerProfileView;

            PlayerProfileData.OnProfileUpdated += UpdateView;
            UpdateView();
        }

        private void UpdateView()
        {
            _playerProfileView.SetAvatar(PlayerProfileData.Avatar);
            _playerProfileView.SetTitle(PlayerProfileData.Title);
            _playerProfileView.SetNickname(PlayerProfileData.Nickname);
            _playerProfileView.SetXp(PlayerProfileData.Xp);
            _playerProfileView.SetSp(PlayerProfileData.Sp);
            _playerProfileView.SetMoney(PlayerProfileData.Money);
            _playerProfileView.SetDiamonds(PlayerProfileData.Diamonds);
            _playerProfileView.SetTickets(PlayerProfileData.Tickets);
        }
        
        public void UpdateProfile(string title = null, string nickname = null, int? xp = null, int? sp = null, int? money = null, int? diamonds = null, int? tickets = null)
        {
            PlayerProfileData.UpdateProfile(title, nickname, xp, sp, money, diamonds, tickets);
        }
    }
}