using System;
using System.Collections.Generic;
using System.Text;

namespace TravelGuide.Intefaces
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
