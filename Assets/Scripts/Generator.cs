using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject foodPrefab;       // Single prefab to spawn
    public float spawnInterval = 2f;    // Time between spawns in seconds
    public float spawnXOffset = 2f;     // Extra distance off-screen to the right

    [Header("Lane Settings")]
    [Tooltip("The three possible Y positions where prefabs can spawn.")]
    public float[] yPositions = new float[] { -0.5f, -3f, -4.5f };

    [Header("Sprite Settings")]
    [Tooltip("Hauteur minimale en pixels souhaitée pour les sprites.")]
    public float minHeightPixels = 100f;

    public string spriteFolder = "Sprites/food"; 

    private float timer;
    private float spawnX;
    private Sprite[] foodSprites;       // Cached array of sprites loaded from Resources

    void Start()
    {
        Camera cam = Camera.main;
        // Calculate screen bounds in world coordinates to find the right edge
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        spawnX = rightEdge.x + spawnXOffset;
  
        // Load all sprites from Resources/Sprites/food
        foodSprites = Resources.LoadAll<Sprite>(spriteFolder);
        

        // Appliquer le Filter Mode "No Filter" (Point) à tous les sprites chargés
        for (int i = 0; i < foodSprites.Length; i++)
        {
            Sprite sprite = foodSprites[i];
            if (sprite != null && sprite.texture != null)
            {
                sprite.texture.filterMode = FilterMode.Point;

                // Si la hauteur en pixels est inférieure à minHeightPixels, on crée un nouveau sprite avec un PPU adapté
                if (sprite.rect.height < minHeightPixels)
                {
                    float newPPU = sprite.pixelsPerUnit * (sprite.rect.height / minHeightPixels);
                    foodSprites[i] = Sprite.Create(
                        sprite.texture,
                        sprite.rect,
                        new Vector2(0.5f, 0.5f),
                        newPPU
                    );
                }
            }
        }
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFood();
            timer = 0f;
        }
    }

    void SpawnFood()
    {
        if (foodPrefab == null) return;
        if (foodSprites == null || foodSprites.Length == 0) return;
        if (yPositions == null || yPositions.Length == 0) return;

        // 1. Pick a random Y position from our array (0, 1, or 2)
        int randomYIndex = Random.Range(0, yPositions.Length);
        float selectedY = yPositions[randomYIndex];

        // 2. Instantiate the prefab at the right side of the screen using the chosen Y position
        Vector3 spawnPosition = new Vector3(spawnX, selectedY, 0f);
        GameObject spawnedObject = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

        // 3. Pick a random sprite and apply it to the SpriteRenderer component because the prefab is just a simple cube, we add sprite later
        int randomSpriteIndex = Random.Range(0, foodSprites.Length);
        SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = foodSprites[randomSpriteIndex];
        spawnedObject.transform.localScale = Vector3.one; // Remet l'échelle à 1 par défaut
     
    }
}