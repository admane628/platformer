using UnityEngine;

public class adapScreen : MonoBehaviour
{

    public float largeurcible=18f;// cases visibles en largeur par exemple
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float rationActuel = (float)Screen.width/Screen.height;
        cam.orthographicSize = largeurcible/(2f*rationActuel);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
