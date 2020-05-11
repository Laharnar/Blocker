public interface ITactic
{
    TacticResult SuggestedResult { get; set; }
    void Simulate();
}
