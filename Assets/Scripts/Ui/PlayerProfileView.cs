using Infra.Helpers;
using TMPro;
using UIAssistant;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class PlayerProfileView : MonoBehaviour
    {
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI xpText;
        [SerializeField] private TextMeshProUGUI nicknameText;
        [SerializeField] private TextMeshProUGUI spText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI diamondsText;
        [SerializeField] private TextMeshProUGUI ticketsText;

        public void SetAvatar(Sprite avatar)
        {
            avatarImage.sprite = avatar;
        }
        
        public void SetTitle(string title)
        {
            titleText.text = title;
        }
        
        public void SetXp(int xp)
        {
            xpText.text = xp.ToString();
        }
        
        public void SetNickname(string nickname)
        {
            nicknameText.text = nickname;
        }
        
        public void SetSp(int sp)
        {
            spText.text = sp.ToString();
        }
        
        public void SetMoney(int money)
        {
            moneyText.text = money.ConvertToCurrencyString();
            moneyText.GetComponent<TextRevealer>()?.RevealText();
        }
        
        public void SetDiamonds(int diamonds)
        {
            diamondsText.text = diamonds.ToString();
        }
        
        public void SetTickets(int tickets)
        {
            ticketsText.text = tickets.ToString();
        }
    }
}