using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title Screen");
    }
    
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("Lose Screen");
    }
    
    public void LoadWinScene()
    {
        SceneManager.LoadScene("Win Screen");
    }
}