using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameModel
{
    public class Player
    {
        public Guid ClientID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PlayerName { get; set; }

    }
}
