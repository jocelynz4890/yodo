using UnityEngine;

public class SimplePathfinding : MonoBehaviour
{
    [SerializeField] public GameObject P1Object;  // Reference to the objects to follow
    [SerializeField] public GameObject P2Object;
    private bool P1Closer = true;
    [SerializeField] private Vector3 P1offset;          // Offset from target position
    [SerializeField] private Vector3 P2offset;
    [SerializeField] private float smoothSpeed = 5f;  // Speed of movement (higher = faster)

    private void FixedUpdate()
    {
        Vector3 currentPos = this.transform.position;
        Vector3 P1Pos = P1Object.transform.position + P1offset;
        Vector3 P2Pos = P2Object.transform.position + P2offset;
        float P1Distance = (currentPos - P1Pos).magnitude;
        float P2Distance = (currentPos - P2Pos).magnitude;
        if (P1Distance >= P2Distance)
        {
            P1Closer = false;
        }
        else
        {
            P1Closer = true;
        }
        float newX = P1Closer ? P1Pos.x : P2Pos.x;
        float newY = P1Closer ? P1Pos.y : P2Pos.y;
        float newZ = P1Closer ? P1Pos.z : P2Pos.z;

        // Only follow on specified axes


        Vector3 desiredPosition = new Vector3(newX, newY, newZ);

        // Smoothly move to target position
        this.transform.position = Vector3.Lerp(currentPos, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}