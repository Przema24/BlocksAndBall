using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private BallMovement ballPrefab;
    [SerializeField] private Transform paddleTransform;
    private Vector3 offset = new Vector3(0, 0.75f, 0);
    private BallMovement ball;

    private void Start()
    {
        SpawnBall();
    }

    private void Update()
    {
        CheckBallFall();
        WaitForReleaseBall();
    }

    private void CheckBallFall()
    {
        if (ball != null)
        {
            if (ball.transform.position.y < -6)
            {
                ball.Reset();
                LifeManager.instance.LoseLife();
            }
        }
    }

    private void WaitForReleaseBall()
    {
        if (PlayerInputController.Instance.ballReleaseButton)
        {
            ball.StartMove();
        }
    }

    private void SpawnBall()
    {
        if (ballPrefab != null)
        {
            ball = MySceneManager.instance.InstantiateInTargetScene(ballPrefab, paddleTransform.transform.position + offset, Quaternion.identity);
        }
    }
}
