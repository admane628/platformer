using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator animator;
    private string[] animationNames = { "Walk", "Fat1", "Fat2", "Fat3", "Fat4" };
    private int currentIndex = 0;

    [SerializeField] private float moveAmount = 1f;
    [SerializeField] private float moveSpeed = 15f; // Increased slightly for snappier response

    private Vector3 targetPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;

        PlayAnimation(currentIndex);
    }

    void Update()
    {

        if (Keyboard.current == null) return;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame && targetPosition.y < -1.5)
        {
            // If we are already moving, base the new target on the current target 
            // so rapid clicks stack up nicely instead of fighting each other.
            targetPosition.y += moveAmount;
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame && targetPosition.y > -3.5)
        {
            targetPosition.y -= moveAmount;
        }
    }

    void FixedUpdate()
    {
        // 2. Perform physics/movement smoothly in FixedUpdate
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
    }

    void PlayAnimation(int index)
    {
        string targetAnimation = animationNames[index];
        animator.Play(targetAnimation);
    }

    public void PlayNextAnimation(){
        // Incrémente l'index sans dépasser la taille du tableau
        int originalIndex = currentIndex ;
        currentIndex = Mathf.Min(currentIndex + 1, animationNames.Length - 1);
        if(originalIndex != currentIndex){
            GameObject fond = GameObject.Find("fond/RawImage");
            BackgroundScroller bg = fond.GetComponent<BackgroundScroller>();
            bg.scrollSpeed = bg.scrollSpeed * 0.8f ; // decrease it by 20%
        }
        PlayAnimation(currentIndex);
    }
}