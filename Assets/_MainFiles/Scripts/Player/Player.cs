using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] CameraShake cameraShake;

 
    bool isSliding;
    bool isJumping;
    float verticalOffset;

    void Start()
    {

    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            StartCoroutine(SlideRoutine());
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // isSliding = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpRoutine());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // StartCoroutine(JumpRoutine());
        }

        Vector3 playerPosition = transform.position;
        playerPosition.y = verticalOffset;
        transform.position = playerPosition;
    }

    void Move()
    {
        float input = Input.GetAxis("Horizontal");
        Vector3 playerPosition = transform.position;

        float strafeSpeed = GameHandler.Instance.strafeSpeed;
        float sideLimit = GameHandler.Instance.sideLimit;

        playerPosition.x += input * strafeSpeed * Time.deltaTime;
        playerPosition.x = Mathf.Clamp(playerPosition.x, -sideLimit, sideLimit);

        transform.position = playerPosition;
    }

    void Slide()
    {
        isSliding = true;
    }

    void Jump()
    {
        isJumping = true;
    }

    IEnumerator JumpRoutine()
    {
        isJumping = true;
        
        float time = 0f;
        float startYPos = transform.position.y;
        float jumpDuration = GameHandler.Instance.jumpDuration;
        AnimationCurve jumpCurve = GameHandler.Instance.jumpCurve;
        float jumpOffset = GameHandler.Instance.jumpOffset;

        while (time < jumpDuration)
        {
            float t = time / jumpDuration;
            verticalOffset = startYPos + jumpCurve.Evaluate(t) * jumpOffset;
            time += Time.deltaTime;
            yield return null;
        }

        verticalOffset = 0;
        isJumping = false;
    }

    IEnumerator SlideRoutine()
    {
        isSliding = true;

        float time = 0f;
        float startYPos = transform.position.y;
        float slideDuration = GameHandler.Instance.slideDuration;
        AnimationCurve slideCurve = GameHandler.Instance.slideCurve;
        float slideOffset = GameHandler.Instance.slideOffset;

        while (time < slideDuration)
        {
            float t = time / slideDuration;
            verticalOffset = startYPos + slideCurve.Evaluate(t) * slideOffset;
            time += Time.deltaTime;
            yield return null;
        }

        verticalOffset = 0;
        isSliding = false;
    }

    void ObstacleCollision(Obstacle obstacle)
    {
        if (!obstacle.GetCanCross())
        {
            Die();
            return;
        }

        if (obstacle.GetSlidePass() && obstacle.GetJumpPass())
        {
            if (isSliding && isJumping)
            {
                Debug.Log("Slide+Jump Cross");
                GameHandler.Instance.AddCrossedObstacleCount();
                return;
            }

            Die();
            return;
        }
        
        if (obstacle.GetSlidePass() && isSliding)
        {
            Debug.Log("Slide Cross");
            GameHandler.Instance.AddCrossedObstacleCount();
            return;
        }

        if (obstacle.GetJumpPass() && isJumping)
        {
            Debug.Log("Jump Cross");
            GameHandler.Instance.AddCrossedObstacleCount();
            return;
        }

        Die();
    }

    void Die()
    {
        Debug.Log("LOL YOU DIED");
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            ObstacleCollision(obstacle);
        }
    }
}
