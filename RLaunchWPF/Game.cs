using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace RLaunch {
    public class Game {

        public string Name { get; set; }
        public string Ver { get; set; }
        public string Desc { get; set; }
        public string Exe { get; set; }

        public object Img { get; set; }

        public Game(string name, string ver, string desc, string exe, string img = "") {
            this.Name = name;
            this.Ver = ver;
            this.Desc = desc;
            this.Exe = exe;
            this.Img = img;
        }


        public void Save() {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText("data/meta/" + $"{this.Name}{this.Ver}.json", json);
        }

        public static Game Load(string filename) {
            string json = File.ReadAllText($"{filename}");
            return JsonConvert.DeserializeObject(json, typeof(Game)) as Game;
        }

    }
}
