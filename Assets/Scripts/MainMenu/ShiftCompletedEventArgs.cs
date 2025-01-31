using System;

namespace MainMenu
{
    public class ShiftCompletedEventArgs : EventArgs
    {
        public int SpRewardForShift { get; }
        
        public ShiftCompletedEventArgs(int spRewardForShift)
        {
            SpRewardForShift = spRewardForShift;
        }
    }
}