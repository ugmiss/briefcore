using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameModel
{
    public enum CenterActionType
    {
        HallAllChat,
        SingleChat,
       
        CreateRoom,
        JoinRoom,

        RoomAllChat,
        LeaveRoom,
        GameReady,
        GameStart,
        
        GameOver,
        GameAction
    }
}
