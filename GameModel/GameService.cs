using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameModel
{
    public class GameService
    {
        private GameService()
        {
        }
        public static readonly GameService Instance = new GameService();

        public static List<Room> RoomList = new List<Room>();

    }
}
