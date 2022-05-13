using System;
using System.Collections.Generic;
using System.Text;

namespace TravelGuide.Converters
{
    public class EmailEntryValidatorBehavior : BaseEntryValidatorBehavior
    {
        protected override string Pattern => @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    }
}
