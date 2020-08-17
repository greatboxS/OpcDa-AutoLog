using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCDataAccess.AppDefinition
{
    public enum OpcDaException
    {
        ERROR,
        OPCDA_HAS_NOT_CREATED,
        SERVER_HAS_NOT_CONNECTED_YET,
        GET_SERVER_LIST_ERROR,
        ADD_CLIENT_GROUP_ERROR,
        GET_CLIENT_GROUP_ITEM_ERROR,
        VALIDATE_GROUP_ITEM_ERROR,
        READ_TAG_ERROR,
        WRITE_TAG_ERROR,
    }
}
