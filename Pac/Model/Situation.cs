namespace Pac.Model
{
    class Situation : IDevice
    {
        private readonly IDevice _devices;

        public Zone Zone { get; set; }
        public string Name { get; set; }

        public Situation(Zone zone, IDevice device)
        {
            Zone = zone;
            _devices = device;
        }
        
        public void On()
        {
            _devices.On();
        }

        public void Off()
        {
            _devices.Off();
        }

        public void Restore()
        {
            _devices.Restore();
        }
    }
}
