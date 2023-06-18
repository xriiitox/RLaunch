using Newtonsoft.Json;
using System.IO;

namespace RLaunchWPF {
    public class Game {

        public string Name { get; set; }
        public string Ver { get; set; }
        public string Desc { get; set; }
        public string Exe { get; set; }
        public string Src { get; set; }
        public string Dir { get; set; }

        public object Img { get; set; }

        public Game(string name, string ver, string desc, string exe, string dir, string src, object img) {
            this.Name = name;
            this.Ver = ver;
            this.Desc = desc;
            this.Exe = exe;
            this.Src = src;
            this.Img = img;
            this.Dir = dir;
        }

        // Use this later to allow config editing maybe
        public void Save() {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText("data/meta/" + $"{this.Name}{this.Ver}.json", json);
        }

        public static Game Load(string filename) {
            var json = File.ReadAllText($"{filename}");
            return JsonConvert.DeserializeObject<Game>(json);
        }

    }
}
