namespace Pac.Model
{
    public class HueBulb : IDevice
    {
        private const int DefaultOn = 100;

        public string Name { get; set; }

        public int LightLevel { get; set; }

        public bool State { get; set; }

        public HueBulb()
        {
            LightLevel = DefaultOn;
        }

        public void On()
        {
            LightLevel = DefaultOn;
            State = true;
        }

        public void Off()
        {
            State = false;
        }

        public void Restore()
        {
            State = true;
        }
    }
}


