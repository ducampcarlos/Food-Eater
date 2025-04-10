using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float steering = 200f;

    private Rigidbody2D rb;
    private float steerDirection = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        steerDirection = 0;

        bool touchActive = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed;

        if (touchActive)
        {
            float touchX = Touchscreen.current.primaryTouch.position.x.ReadValue();
            steerDirection = (touchX < Screen.width / 2f) ? 1f : -1f;
        }

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            steerDirection = 1f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            steerDirection = -1f;
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.gameStarted)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }

        rb.AddForce(transform.up * acceleration, ForceMode2D.Force);
        rb.MoveRotation(rb.rotation + steerDirection * steering * Time.fixedDeltaTime);
    }
}
