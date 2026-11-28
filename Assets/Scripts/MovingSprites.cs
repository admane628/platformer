using UnityEngine;

public class MovingSprites : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 7f;
    public bool toDestroy = true;
    private float leftBoundaryX;

    void Start()
    {
        Camera cam = Camera.main;
        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        leftBoundaryX = leftEdge.x - 1f;
        
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
        if (toDestroy && collision.gameObject.name == "Player"){
            Player player = collision.gameObject.GetComponent<Player>();

            if(gameObject.name.Contains("badfood")){
                Debug.Log("badfood :( ");
                player.PlayNextAnimation();                
            }
            Destroy(gameObject);
        }
    }
}