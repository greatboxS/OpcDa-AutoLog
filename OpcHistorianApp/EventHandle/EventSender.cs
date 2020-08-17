using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace OpcHistorianApp
{
    public class EventSender
    {
        public EventSender(bool selected, TagProperty prop)
        {
            TagSelection = new TagSelection(selected, prop);
        }
        public TagSelection TagSelection { get; set; }
    }

    public class TagSelection
    {
        public bool Selected { get; set; }
        public TagProperty TagProp { get; set; }

        public TagSelection(bool selected, TagProperty prop )
        {
            Selected = selected;
            TagProp = prop;
        }
    }
}
