using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] CameraShake cameraShake;
    [Header("Debug info. Set In GameHandler!")]
    [SerializeField] float strafeSpeed = 5f;
    [SerializeField] float sideLimit = 5f;

    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpDuration = 3f;
    [SerializeField] float jumpOffset = 2f;

    [SerializeField] AnimationCurve slideCurve;
    [SerializeField] float slideDuration = 3f;
    [SerializeField] float slideOffset = -1.5f;
 
    bool isSliding;
    bool isJumping;
    float verticalOffset;

    void Start()
    {
        UpdatePlayerSettings(GameHandler.Instance.GetPlayerSettings());
        GameHandler.Instance.OnPlayerSettingsUpdate += UpdatePlayerSettings;
        GameHandler.Instance.OnBuffGet += GetBuff;
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

    void UpdatePlayerSettings(PlayerSettingsSO playerSettings)
    {
        strafeSpeed = playerSettings.strafeSpeed;
        sideLimit = playerSettings.sideLimit;
        jumpCurve = playerSettings.jumpCurve;
        jumpDuration = playerSettings.jumpDuration;
        jumpOffset = playerSettings.jumpOffset;
        slideCurve = playerSettings.slideCurve;
        slideDuration = playerSettings.slideDuration;
        slideOffset = playerSettings.slideOffset;

        cameraShake.SetStepsPerMinute(playerSettings.stepsPerMinute);

        SectorsHandler sectorsHandler = GameHandler.Instance.GetSectorHandlerByType("Main");
        sectorsHandler.SetRunSpeed(playerSettings.runSpeed);
    }

    void GetBuff(BuffStatsSO buffStats)
    {
        strafeSpeed += buffStats.strafeSpeed;
        sideLimit += buffStats.sideLimit;        
        jumpDuration += buffStats.jumpDuration;
        jumpOffset += buffStats.jumpOffset;        
        slideDuration += buffStats.slideDuration;
        slideOffset += buffStats.slideOffset;

        SectorsHandler sectorsHandler = GameHandler.Instance.GetSectorHandlerByType("Main");
        sectorsHandler.AddRunSpeed(buffStats.runSpeed);
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            ObstacleCollision(obstacle);
        }
    }
}
