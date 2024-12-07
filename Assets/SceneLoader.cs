using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("Main Scene");

        // OR use scene index number:
        // SceneManager.LoadScene(1);
    }
    public void LoadTitleScene()
    {
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("Title Screen");

        // OR use scene index number:
        // SceneManager.LoadScene(1);
    }
}