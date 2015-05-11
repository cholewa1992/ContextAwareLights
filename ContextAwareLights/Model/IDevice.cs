namespace ContextAwareLights.Model
{
    public interface IDevice
    {
        string Identifier { get; set; }
        void On();
        void Off();
        void Restore();
    }
}
