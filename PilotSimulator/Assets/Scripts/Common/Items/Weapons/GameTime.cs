using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] bool paused;
    public void ResumeGame()
    {
        PauseGlobal.Instance.IsPaused = false;
        paused = false;
    }

    public void PauseGame()
    {
        PauseGlobal.Instance.IsPaused = true;
        paused = true;
    }
}
