namespace Pac.Model
{
    public interface IDevice
    {
        string Name { get; set; }
        void On();
        void Off();
        void Restore();
    }
}