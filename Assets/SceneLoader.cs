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
}