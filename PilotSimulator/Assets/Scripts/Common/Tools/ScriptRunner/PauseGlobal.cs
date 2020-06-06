public class PauseGlobal
{
    static PauseGlobal instance;

    public static PauseGlobal Instance {
        get {
            if (instance == null)
                instance = new PauseGlobal();
            return instance;
        }
    }
    public PauseGlobal()
    {
        IsPaused = false;
    }

    public bool IsPaused { get; set; }
}
