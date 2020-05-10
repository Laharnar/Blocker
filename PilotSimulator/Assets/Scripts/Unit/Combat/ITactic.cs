public interface ITactic
{
    public TacticResult SuggestedResult { get; set; }
    public void Simulate();
}
