using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/PlayerProfile", order = 1)]
    public class PlayerProfileSo : ScriptableObject
    {
        [SerializeField] private Sprite avatar;
        [SerializeField] private string title;
        [SerializeField] private string nickname;
        [SerializeField] private int xp;
        [SerializeField] private int sp;
        [SerializeField] private int money;
        [SerializeField] private int diamonds;
        [SerializeField] private int tickets;
        [SerializeField] private int stamina;

        public event Action OnProfileUpdated;

        public Sprite Avatar => avatar;
        public string Title => title;
        public string Nickname => nickname;
        public int Xp => xp;
        public int Sp => sp;
        public int Money => money;
        public int Diamonds => diamonds;
        public int Tickets => tickets;
        public int Stamina => stamina;

        public void UpdateAvatar(Sprite newAvatar)
        {
            if (newAvatar != avatar)
            {
                avatar = newAvatar;
                OnProfileUpdated?.Invoke();
            }
        }

        public void UpdateTitle([CanBeNull] string newTitle)
        {
            if (newTitle != title)
            {
                title = newTitle;
                OnProfileUpdated?.Invoke();
            }
        }

        public void UpdateNickname([CanBeNull] string newNickname)
        {
            if (newNickname != nickname)
            {
                nickname = newNickname;
                OnProfileUpdated?.Invoke();
            }
        }

        private bool TryModifyValue(ref int property, int delta, bool set)
        {
            if (set)
            {
                property = delta;
                OnProfileUpdated?.Invoke();
                return true;
            }

            return ModifyValue(ref property, delta);
        }
        
        private bool ModifyValue(ref int property, int delta)
        {
            if (delta == 0)
            {
                return false;
            }

            property += delta;
            OnProfileUpdated?.Invoke();
            return true;
        }

        public bool ModifyXp(int delta, bool set) => TryModifyValue(ref xp, delta, set);
        public bool ModifySp(int delta, bool set) => TryModifyValue(ref sp, delta, set);
        public bool ModifyMoney(int delta, bool set) => TryModifyValue(ref money, delta, set);
        public bool ModifyDiamonds(int delta, bool set) => TryModifyValue(ref diamonds, delta, set);
        public bool ModifyTickets(int delta, bool set) => TryModifyValue(ref tickets, delta, set);
        public bool ModifyStamina(int delta, bool set) => TryModifyValue(ref stamina, delta, set);
    }
}
