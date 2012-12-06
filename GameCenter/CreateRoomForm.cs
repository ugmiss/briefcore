using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameModel;

namespace GameCenter
{
    public partial class CreateRoomForm : Form
    {
        public CreateRoomForm()
        {
            InitializeComponent();
        }
        public Room Room { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            room.RoomName = textBox1.Text;
            room.RoomID = Guid.NewGuid();
            room.RoomMaster = ClientCache.CurrentPlayer;
            room.PlayerList = new List<Player>();
            room.PlayerList.Add(ClientCache.CurrentPlayer);
            room.PersonCount = textBox2.Text.ParseTo<int>();
            room.RoomType = comboBox1.SelectedText;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
