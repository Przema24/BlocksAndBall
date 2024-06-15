using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 4;
    private float speedMultiplier = 1;
    private PaddleMovement paddle;
    private bool gameStarted = false;
    private bool canChangeDirection = true;

    private Vector3 offset = new Vector3(0, 0.75f, 0);
    private Vector2 startedDirection = new Vector2(1, 1);
    private Vector2 direction;

    private void Start()
    {
        direction = startedDirection;
        paddle = FindObjectOfType<PaddleMovement>();
    }

    private void Update()
    {
        if (gameStarted == false)
        {
            StickToPaddle();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        increaseSpeedMultiplier();

        if (canChangeDirection)
        {
            canChangeDirection = false;
            StartCoroutine(ChangeDirection(collision));
        }

        IHittable hittable = collision.gameObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            collision.gameObject.GetComponent<IHittable>().Hit();
        }
    }

    private IEnumerator ChangeDirection(Collision2D collision)
    {

        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;

        Vector2 reflectDirection = Vector2.Reflect(direction, normal);
        rb.velocity = reflectDirection * speed * speedMultiplier;
        direction = rb.velocity.normalized;

        yield return new WaitForSeconds(0.02f);

        canChangeDirection = true;
    }

    private void StickToPaddle()
    {
        gameObject.transform.position = paddle.transform.position + offset;
    }

    private void increaseSpeedMultiplier()
    {
        speedMultiplier += 0.05f;
        if (speedMultiplier > 2.5f)
        {
            speedMultiplier = 2.5f;
        }
    }

    public void StartMove()
    {
        if (gameStarted == false)
        {
            gameStarted = true;
            var startVelocity = direction * speed * speedMultiplier;
            rb.velocity = startVelocity;
        }
    }

    public void Reset()
    {
        gameStarted = false;
        direction = startedDirection;
        speedMultiplier = 1;
    }
}
