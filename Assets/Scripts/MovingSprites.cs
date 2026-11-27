using UnityEngine;

public class MovingSprites : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    private float leftBoundaryX;

    void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            // Calculate the left edge of the camera view in world coordinates
            // We subtract an extra buffer (-1f) so the prefab fully exits the screen before being destroyed
            Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            leftBoundaryX = leftEdge.x - 1f;
        }
        else
        {
            // Fallback default coordinate if no camera is found
            leftBoundaryX = -15f;
        }
    }

    void Update()
    {
        // Move continuously from right to left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy if outside the left border of the camera
        if (transform.position.x < leftBoundaryX)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}