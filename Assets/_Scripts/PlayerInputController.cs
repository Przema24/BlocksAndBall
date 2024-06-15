using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset paddleControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Paddle";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string releaseBall = "ReleaseBall";

    private InputAction moveAction;
    private InputAction ballReleaseAction;

    public Vector2 moveInput {  get; private set; }
    public bool ballReleaseButton {  get; private set; }

    //[SerializeField] private PaddleMovement objectToMove;
    [SerializeField] private BallSpawner ballSpawner;

    public static PlayerInputController Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = paddleControls.FindActionMap(actionMapName).FindAction(move);
        ballReleaseAction = paddleControls.FindActionMap(actionMapName).FindAction(releaseBall);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        ballReleaseAction.performed += context => ballReleaseButton = true;
        ballReleaseAction.canceled += context => ballReleaseButton = false;
    }

    

    private void OnEnable()
    {
        moveAction.Enable();
        ballReleaseAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        ballReleaseAction.Disable();
    }
}
