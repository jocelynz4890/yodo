using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int alivePlayers { get; set; }
    private SceneLoader _sceneLoader;
    
    void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
        alivePlayers = 2;
    }
    
    void FixedUpdate()
    {
        if (alivePlayers == 0)
        {
            _sceneLoader.LoadLoseScene();
        }
    }
    
}
