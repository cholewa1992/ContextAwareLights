namespace Pac.Model
{
    internal interface ISituation
    {
        Zone Zone { get; set; }
        string Name { get; set; }
        void On();
        void Off();
    }
}