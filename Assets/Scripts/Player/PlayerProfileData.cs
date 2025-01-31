using System;

namespace Player
{
    [Serializable]
    public class PlayerProfileData
    {
        public string title;
        public string nickname;
        public int xp;
        public int sp;
        public int money;
        public int diamonds;
        public int tickets;
        public int stamina;
        public string avatarPath;
    }
}