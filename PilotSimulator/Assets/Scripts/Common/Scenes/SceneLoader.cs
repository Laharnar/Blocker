using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int loadId;
    [SerializeField] string loadName;
    public void LoadDefault()
    {
        if (loadName != "")
            LoadScene(loadName);
        else if (loadId >= 0)
            LoadScene(loadId);
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
