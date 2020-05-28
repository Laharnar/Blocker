public interface IUIValue
{
    bool IsChanged { get; set; }

    string GetContent();
}
