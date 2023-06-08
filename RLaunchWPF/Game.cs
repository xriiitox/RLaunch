using Newtonsoft.Json;
using System.IO;

namespace RLaunch {
    public class Game {

        public string name { get; set; }
        public string ver { get; set; }
        public string desc { get; set; }
        public string exe { get; set; }

        public string? img;

        public Game(string name, string ver, string desc, string exe, string? img = null) {
            this.name = name;
            this.ver = ver;
            this.desc = desc;
            this.exe = exe;
            this.img = img;
        }


        public void Save() {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText("data/meta/" + $"{this.name}{this.ver}.json", json);
        }

        public static Game Load(string filename) {
            string json = File.ReadAllText($"{filename}");
            return JsonConvert.DeserializeObject(json, typeof(Game)) as Game;
        }

    }
}
