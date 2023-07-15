using Newtonsoft.Json;
using System.IO;

namespace RLaunchWPF {
    public class Game {

        public string Name { get; }
        public string Ver { get; }
        public string Desc { get; }
        public string Exe { get; }
        public string Src { get; }
        public string Dir { get; }

        public object Img { get; set; }

        public Game(string name, string ver, string desc, string exe, string dir, string src, object img) {
            Name = name;
            Ver = ver;
            Desc = desc;
            Exe = exe;
            Src = src;
            Img = img;
            Dir = dir;
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
