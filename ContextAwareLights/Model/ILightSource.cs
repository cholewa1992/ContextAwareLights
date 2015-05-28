namespace ContextAwareLights.Model
{
    public interface ILightSource
    {
        string Identifier { get; set; }
        void On();
        void Off();
        void Restore();
    }
}
