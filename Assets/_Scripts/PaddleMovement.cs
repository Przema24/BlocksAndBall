using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.velocity = PlayerInputController.Instance.moveInput * speed;
    }
}
