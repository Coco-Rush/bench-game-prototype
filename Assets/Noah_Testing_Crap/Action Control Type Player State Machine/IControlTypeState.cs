public interface IControlTypeState
{
    public bool enabled { get; set; }
    void ExitState();
    void EnterState();
}
