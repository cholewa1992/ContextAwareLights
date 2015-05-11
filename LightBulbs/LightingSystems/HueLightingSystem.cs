using System;
using System.Collections.Generic;
using System.Drawing;
using Q42.HueApi;
using Q42.HueApi.Interfaces;

namespace LightBulbs.LightingSystems
{
    public class HueLightingSystem: ILightingSystem
    {

        private HueClient _client;

        public HueLightingSystem(string name, string ip)
        {
            //var bridges = GetBridges();

            
            _client = new HueClient(ip);
            _client.RegisterAsync("ubilight", "ubilightkey");
            _client.Initialize("ubilightkey");
            Name = name;
        }

        private IEnumerable<string> GetBridges()
        {
            IBridgeLocator locator = new HttpBridgeLocator();

            //For Windows 8 and .NET45 projects you can use the SSDPBridgeLocator which actually scans your network. 
            //See the included BridgeDiscoveryTests and the specific .NET and .WinRT projects
            return locator.LocateBridgesAsync(TimeSpan.FromSeconds(3)).Result;

        }

        public string Name { get; set; }

        public List<LightSource> GetSources()
        {
            var list = new List<LightSource>();

            foreach (var r in _client.GetLightsAsync().Result)
            {
                list.Add(new LightSource(this, r.Id));
            }

            return list;

        }

        public void SetColor(LightSource ls, Color c)
        {
            var colorHex = Helpers.ColorToHex(c);
            _client.SendCommandAsync(new LightCommand().SetColor(colorHex), new[] { ls.SystemId });
        }

        public void TurnOff(LightSource ls)
        {
            _client.SendCommandAsync(new LightCommand().TurnOff(), new []{ls.SystemId});
        }

        public void TurnOn(LightSource ls)
        {
            _client.SendCommandAsync(new LightCommand().TurnOn(), new[] { ls.SystemId });
        }

        public bool GetOn(LightSource ls)
        {
            return _client.GetLightAsync(ls.SystemId).Result.State.On;
        }

        public void SetLevel(LightSource ls, double level)
        {
            if (0.0 > level){throw new ArgumentException("level can't be under 0.0!");}
            if (level > 1.0){throw new ArgumentException("level can't be over 1.0!");}

            byte b = Convert.ToByte(level*254);

            _client.SendCommandAsync(new LightCommand() { Brightness = b }, new[] { ls.SystemId });
        }

        public double GetLevel(LightSource ls)
        {
            byte b = _client.GetLightAsync(ls.SystemId).Result.State.Brightness;
            return Math.Round(Convert.ToDouble(b)/255, 2);
        }

        public Color GetColor(LightSource ls)
        {
            throw new NotImplementedException();
            return Helpers.HexToColor(_client.GetLightAsync(ls.SystemId).Result.State.ToHex());
        }

        
    }
}
