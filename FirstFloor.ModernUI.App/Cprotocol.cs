using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public enum PROTOCOL : short
	{
		BEGIN = 0,

		CHAT_MSG_REQ = 1,
		CHAT_MSG_ACK = 2,
        CHAT_MSG_INSERT = 3,
        CHAT_MSG_INSERT_ACK = 4,
        CHAT_MSG_DELETE = 5,
        CHAT_MSG_DELETE_ACK = 6,
        CHAT_MSG_UPDATE = 7,
        CHAT_MSG_UPDATE_ACK = 8,
        END
    }
}
