using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float waitTime = 3f;
    private EnemyAnimatorService animator;
    private new Rigidbody2D rigidbody;
    private bool isMovingRight = true;
    // private float lastPosX;
    // private float currentPosX;
    // private Vector3 currentLocalScale;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<EnemyAnimatorService>();

        startPosition = transform.position;
        leftPoint = new Vector2(startPosition.x - maxDistance, startPosition.y);
        rightPoint = new Vector2(startPosition.x + maxDistance, startPosition.y);

        // flip
        // lastPosX = transform.position.x;
        // currentLocalScale = transform.localScale;
    }

    void Start()
    {
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            Vector2 target = isMovingRight ? rightPoint : leftPoint;
            while (Vector2.Distance(transform.position, target) > 0.1f)
            {
                Move(target);
                animator.SetAnimRun(true);
                yield return null;
            }

            animator.SetAnimRun(false);
            yield return new WaitForSeconds(waitTime);
            isMovingRight = !isMovingRight;

            float localScaleX = transform.localScale.x;
            float localScaleY = transform.localScale.y;

            transform.localScale = new Vector3(localScaleX * (-1), localScaleY, 1);
        }
    }

    private void Move(Vector2 target)
    {
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.deltaTime);
        rigidbody.MovePosition(newPos);
    }

    // private void Flip()
    // {
    //     currentPosX = transform.position.x;

    //     if (currentPosX > lastPosX)
    //     {
    //         transform.localScale = new Vector3(Mathf.Abs(currentLocalScale.x), currentLocalScale.y, currentLocalScale.z);
    //     }
    //     else if (currentPosX < lastPosX)
    //     {
    //         transform.localScale = new Vector3(-Mathf.Abs(currentLocalScale.x), currentLocalScale.y, currentLocalScale.z);
    //     }
    //     lastPosX = currentPosX;
    // }
}