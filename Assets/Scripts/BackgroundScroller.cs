using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundScroller : MonoBehaviour
{
    private RawImage rawImage;
    public float scrollSpeed = 15f;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        // Move the UV rect continuously along the X axis
        Rect uvRect = rawImage.uvRect;
        uvRect.x += scrollSpeed / 100f * Time.deltaTime;
        rawImage.uvRect = uvRect;
    }
}