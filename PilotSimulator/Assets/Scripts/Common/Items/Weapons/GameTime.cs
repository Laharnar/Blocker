using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] bool paused;
    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
    }
}
