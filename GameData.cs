using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    [Serializable()]
    public class GameData
    {
        public int Gameid { get; set; }
        public string Gamepath { get; set; }
        public string Gameargs { get; set; }
        public string Gameicon { get; set; }
        public string Gamebackground { get; set; }

    }
}
