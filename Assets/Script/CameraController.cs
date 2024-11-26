using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform player;

    [Header("Camera Settings")]
    public float followSpeed = 5f;
    public Vector2 offset;
    public Vector2 deadZone = new Vector2(2f, 1f);

    [Header("Boundaries")]
    public bool useBoundaries = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 targetPosition;

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate target position with offset
        targetPosition = player.position + (Vector3)offset;

        // Apply Dead Zone
        Vector3 currentPos = transform.position;
        if (Mathf.Abs(targetPosition.x - currentPos.x) > deadZone.x)
        {
            currentPos.x = Mathf.Lerp(currentPos.x, targetPosition.x, followSpeed * Time.deltaTime);
        }
        if (Mathf.Abs(targetPosition.y - currentPos.y) > deadZone.y)
        {
            currentPos.y = Mathf.Lerp(currentPos.y, targetPosition.y, followSpeed * Time.deltaTime);
        }

        // Apply Boundaries
        if (useBoundaries)
        {
            currentPos.x = Mathf.Clamp(currentPos.x, minBounds.x, maxBounds.x);
            currentPos.y = Mathf.Clamp(currentPos.y, minBounds.y, maxBounds.y);
        }

        // Update camera position
        transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);
    }

    public void SetBoundaries(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
        useBoundaries = true;
    }
}
