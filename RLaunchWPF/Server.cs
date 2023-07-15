using System.IO;
using System.Runtime.InteropServices.Marshalling;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RLaunchWPF; 



public class Server {
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Connection {
        Ssh,
        Web,
        Telnet
    }
    
    public string Name { get; set; }
    public string[] Games { get; set; }
    
    public string IP { get; set; }
    public int Port { get; set; }
    public string Desc { get; set; }
    public string SshUser { get; set; }

    public Connection ConnectionMethod { get; set; }

    public Server(string name, string[] games, string ip, string desc, string sshUser, int port, Connection connectionMethod) {
        Name = name;
        Games = games;
        IP = ip;
        Port = port;
        Desc = desc;
        SshUser = sshUser;
        ConnectionMethod = connectionMethod;
    }
    
    public static Server Load(string filename) {
        var json = File.ReadAllText($"{filename}");
        return JsonConvert.DeserializeObject<Server>(json);
    }
}