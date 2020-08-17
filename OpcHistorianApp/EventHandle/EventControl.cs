using OPCDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcHistorianApp
{
    public class EventControl
    {
        public static event HandleEvent TagSelected;
        public static event HandleEvent AllTagSelected;
        public static event HandleEvent NewConfiguration;
        public static event HandleEvent NewGroupChanged;
        public static event HandleEvent GroupSaveChanged;
        public static event HandleEvent AddGroupCompleted;
        public static event HandleEvent UpdateCurrentGroupEvent;
        public static event HandleEvent UpdateTagListEvent;

        public static void TagItemSelectedChanged(object sensder)
        {
            if (TagSelected != null)
                TagSelected.Invoke(sensder, new EventControl());
        }

        public static void SelectAllTags(bool selectAll)
        {
            if (AllTagSelected != null)
                AllTagSelected.Invoke(selectAll, new EventControl());
        }

        public static void ConfigurationState(bool state)
        {
            if (NewConfiguration != null)
                NewConfiguration.Invoke(state, new EventControl());
        }

        public static void AddNewGroup(LoggingGroup newGroup, bool autoNameGeneration)
        {
            if (NewGroupChanged != null)
                NewGroupChanged.Invoke(newGroup, autoNameGeneration);
        }

        public static void SaveChanged(LoggingGroup group)
        {
            if (GroupSaveChanged != null)
                GroupSaveChanged.Invoke(group, null);
        }

        public static void AddGroupComplete(IList<LoggingGroup> groups)
        {
            if (AddGroupCompleted != null)
                AddGroupCompleted.Invoke(groups, null);
        }

        public static void UpdateCurrentGroup(int groupId, IList<TagProperty> tags)
        {
            if (UpdateCurrentGroupEvent != null)
                UpdateCurrentGroupEvent.Invoke(tags, groupId);
        }

        public static void UpdateTagList(IList<TagProperty> tags, int id)
        {
            if (UpdateTagListEvent != null)
                UpdateTagListEvent.Invoke(tags, id);
        }
    }

    public delegate void HandleEvent(object sender, object userObject);
}
