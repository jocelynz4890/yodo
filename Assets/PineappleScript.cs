using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleScript : MonoBehaviour
{
    public float SecUntilPineappleFinish = 120f;
    public float SecForPineappleGrowth = 30f;
    public bool IsPaused = false;
    public float GrowthScale = 1f;
    public bool IsFullSize = false;

    private float growthTime = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsPaused)
        {
            growthTime += Time.fixedDeltaTime;
            SecUntilPineappleFinish -= Time.fixedDeltaTime;

            if (growthTime >= SecForPineappleGrowth)
            {
                growthTime = 0f;
                transform.localScale += new Vector3(GrowthScale, GrowthScale, GrowthScale);
            }
            
            if (SecUntilPineappleFinish <= 0f)
            {
                IsFullSize = true;
            }
        }
    }
}

