using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundScroller : MonoBehaviour
{
    private RawImage rawImage;
    [SerializeField] private float scrollSpeed = 0.2f;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        // Move the UV rect continuously along the X axis
        Rect uvRect = rawImage.uvRect;
        uvRect.x += scrollSpeed * Time.deltaTime;
        rawImage.uvRect = uvRect;
    }
}