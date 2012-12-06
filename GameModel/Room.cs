using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameModel
{
    public class Room
    {
        public Guid RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int PersonCount { get; set; }
        public List<Player> PlayerList = new List<Player>();
        public Player RoomMaster { get; set; }
        public IGame Game { get; set; }
    }
}
