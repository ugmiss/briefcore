using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.IO;
using Newtonsoft.Json;
namespace MyCutter
{
    public class Test
    {
        public static void Try()
        {
            Book book = new Book();
            book.Height = 768;
            book.Width = 1024;
            book.BookName = "小鬼当家";
            // 第一页
            Page rootpage = new Page();
            rootpage.Name = "第一页";
            Block block = new Block();
            block.BlockType = EnumBlockType.Image.ToString();
            block.Height = 500;
            block.Width = 700;
            block.X = 50;
            block.Y = 30;
            block.BlockResource = "/resources/back.jpg";
            Action act = new Action();
            act.ActionType = EnumAction.Touch.ToString();
            act.Event = EnumEvent.PlayAudio.ToString();
            act.ActionResource = "/resources/sound.mp3";
            block.Actions = new List<Action>();
            block.Actions.Add(act);
            rootpage.Blocks = new List<Block>();
            rootpage.Blocks.Add(block);

            block = new Block();
            block.BlockType = EnumBlockType.Word.ToString();
            block.Height = 300;
            block.Width = 400;
            block.X = 150;
            block.Y = 830;
            block.BlockResource = "/resources/story.txt";
            rootpage.Blocks.Add(block);

            // 第二页
            Page secondpage = new Page();
            secondpage.Name = "第二页";
            block = new Block();
            block.BlockType = EnumBlockType.Image.ToString();
            block.Height = 50;
            block.Width = 70;
            block.X = 50;
            block.Y = 30;
            block.BlockResource = "/resources/wheel.jpg";
            act = new Action();
            act.ActionType = EnumAction.Blow.ToString();
            act.Event = EnumEvent.Move.ToString();
            act.ActionDetail = "{move(left:200|down:20|speed:600);rotate(x:200|y:400|angle:360|speed:1000|anticlockwise:false|replay:true)";
            //act.EventResource = "/resources/sound.mp3";
            block.Actions = new List<Action>();
            block.Actions.Add(act);
            secondpage.Blocks = new List<Block>();
            secondpage.Blocks.Add(block);

            block = new Block();
            block.BlockType = EnumBlockType.Word.ToString();
            block.Height = 300;
            block.Width = 400;
            block.X = 150;
            block.Y = 830;
            block.BlockResource = "/resources/story.txt";
            secondpage.Blocks.Add(block);

            string path = AppDomain.CurrentDomain.BaseDirectory + "/" + book.BookName + "/";
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            Newtonsoft.Json.JsonConvert.SerializeObject(book).WriteToFile(path + book.BookName + ".json");
            Newtonsoft.Json.JsonConvert.SerializeObject(rootpage).WriteToFile(path + rootpage.Name + ".json");
            Newtonsoft.Json.JsonConvert.SerializeObject(secondpage).WriteToFile(path + secondpage.Name + ".json");
            Zip.ZipFolder(path, AppDomain.CurrentDomain.BaseDirectory + "/" + book.BookName + ".zip");
        }
    }
}
