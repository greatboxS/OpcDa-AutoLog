using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcHistorianApp
{
    public class EventSender
    {
        public EventSender(bool selected, OpcDaItem prop)
        {
            TagSelection = new TagSelection(selected, prop);
        }
        public TagSelection TagSelection { get; set; }
    }

    public class TagSelection
    {
        public bool Selected { get; set; }
        public OpcDaItem TagProp { get; set; }

        public TagSelection(bool selected, OpcDaItem prop )
        {
            Selected = selected;
            TagProp = prop;
        }
    }
}
