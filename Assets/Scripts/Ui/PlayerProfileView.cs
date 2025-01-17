using System;
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

        public RectTransform GetRectOfElement(EPlayerProfileViewElement viewElement)
        {
            return viewElement switch
            {
                EPlayerProfileViewElement.Diamonds => diamondsText.rectTransform,
                EPlayerProfileViewElement.Money => moneyText.rectTransform,
                EPlayerProfileViewElement.Nickname => nicknameText.rectTransform,
                EPlayerProfileViewElement.Sp => spText.rectTransform,
                EPlayerProfileViewElement.Xp => xpText.rectTransform,
                EPlayerProfileViewElement.Tickets => ticketsText.rectTransform,
                EPlayerProfileViewElement.Title => titleText.rectTransform,
                _ => null,
            };
        }
        
        public void SetAvatar(Sprite avatar)
        {
            avatarImage.sprite = avatar;
        }

        public void SetTitle(string title)
        {
            UpdateTextElement(titleText, title);
        }

        public void SetNickname(string nickname)
        {
            UpdateTextElement(nicknameText, nickname);
        }

        public void SetXp(int xp)
        {
            UpdateTextElement(xpText, xp.ToString());
        }

        public void SetSp(int sp)
        {
            UpdateTextElement(spText, sp.ToString());
        }

        public void SetMoney(int money)
        {
            UpdateTextElement(moneyText, money.ConvertToCurrencyString(), () =>
            {
                moneyText.GetComponent<TextRevealer>()?.RevealText();
            });
        }

        public void SetDiamonds(int diamonds)
        {
            UpdateTextElement(diamondsText, diamonds.ToString());
        }

        public void SetTickets(int tickets)
        {
            UpdateTextElement(ticketsText, tickets.ToString());
        }

        private void UpdateTextElement(TextMeshProUGUI textComponent, string newText, Action onChangeAction = null)
        {
            if (textComponent.text != newText)
            {
                textComponent.text = newText;
                onChangeAction?.Invoke(); 
            }
        }
    }
}