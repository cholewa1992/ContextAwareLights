using System;
using System.Collections.Generic;
using System.Drawing;
using Q42.HueApi;
using Q42.HueApi.Interfaces;

namespace LightBulbs.LightingSystems
{
    public static class HueLightingSystem
    {

        public static HueClient GetInstance(string ip)
        {
            var instance = new HueClient(ip);
            instance.RegisterAsync("ubilight", "ubilightkey");
            instance.Initialize("ubilightkey");
            return instance;
        }

    }
}
