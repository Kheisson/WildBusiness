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
        
        public event Action OnProfileUpdated;
        
        public Sprite Avatar => avatar;
        public string Title => title;
        public string Nickname => nickname;
        public int Xp => xp;
        public int Sp => sp;
        public int Money => money;
        public int Diamonds => diamonds;
        public int Tickets => tickets;

        public void UpdateProfile([CanBeNull] string title, [CanBeNull] string nickname, int? xp, int? sp, int? money, int? diamonds, int? tickets)
        {
            this.title = title ?? this.title;
            this.nickname = nickname ?? this.nickname;
            this.xp = xp ?? this.xp;
            this.sp = sp ?? this.sp;
            this.money = money ?? this.money;
            this.diamonds = diamonds ?? this.diamonds;
            this.tickets = tickets ?? this.tickets;

            OnProfileUpdated?.Invoke();
        }
    }
}