public interface ITactic
{
    TacticResult SuggestedResult { get; set; }
    void Simulate();
    void Activate();
    void Deactivate();
}
