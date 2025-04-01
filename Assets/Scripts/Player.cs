using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateAmount = 5f;
    float rot = 0;
    int score = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rot = 0; // Resetear la rotación en cada frame

#if UNITY_ANDROID || UNITY_EDITOR
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            float touchX = Touchscreen.current.primaryTouch.position.x.ReadValue();
            rot = rotateAmount * ((touchX < Screen.width / 2f) ? 1 : -1);
        }
#elif UNITY_STANDALONE || UNITY_EDITOR
        if (Mouse.current.leftButton.isPressed)
        {
            float mouseX = Mouse.current.position.x.ReadValue();
            rot = rotateAmount * ((mouseX < Screen.width / 2f) ? 1 : -1);
        }
#endif
    }

    private void FixedUpdate()
    {
        if (rot != 0)
        {
            transform.Rotate(0, 0, rot);
        }

        // Asegurar que no haya rotación no deseada
        rb.angularVelocity = 0;
        rb.linearVelocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            collision.gameObject.SetActive(false); // Desactiva antes de destruir
            Destroy(collision.gameObject, 0.1f);  // Espera un poco antes de destruir
            score++;

            if (score >= 5)
            {
                print("You Win!");
            }
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
