using System;

namespace MainMenu
{
    public class ChequeCollectedEventArgs : EventArgs
    {
        public int ChequeValue { get; }
        
        public ChequeCollectedEventArgs(int chequeValue)
        {
            ChequeValue = chequeValue;
        }
    }
}