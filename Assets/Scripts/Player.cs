using UnityEngine;
using UnityEngine.InputSystem;

public class DirectAnimationSwitcher : MonoBehaviour
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

        currentIndex = 0;
        PlayCurrentAnimation();
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

    void PlayCurrentAnimation()
    {
        string targetAnimation = animationNames[currentIndex];
        animator.Play(targetAnimation);
    }
}