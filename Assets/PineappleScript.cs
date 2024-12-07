using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleScript : MonoBehaviour
{
    [SerializeField] private float GameTimer = 0f;
    [SerializeField] public bool IsPaused = false;
    [SerializeField] public float SecPerPineappleGrowth = 10f;
    [SerializeField] public float GrowthScale = 1f;
    [SerializeField] public bool IsFullSize = false;
    [SerializeField] public float FullSize = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsPaused)
        {
            GameTimer += Time.fixedDeltaTime;
            if (GameTimer >= SecPerPineappleGrowth && !IsFullSize)
            {
                GameTimer = 0f;
                transform.localScale += new Vector3(GrowthScale,GrowthScale,GrowthScale);
                if (transform.localScale.x > FullSize)
                {
                    IsFullSize = true;
                }
            }
        }
    }
}
