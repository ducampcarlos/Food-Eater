using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float steering = 100f;
    [SerializeField] private float directionAdjustSpeed = 1f;
    [SerializeField] private float maxDistanceFromPlayer = 30f;

    private Rigidbody2D rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }


    private void Update()
    {
        if (player == null || !GameManager.Instance.gameStarted) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > maxDistanceFromPlayer)
        {
            gameObject.SetActive(false);
        }
    }


    private void FixedUpdate()
    {
        if (!GameManager.Instance.gameStarted || player == null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }

        // Movimiento hacia adelante
        rb.AddForce(transform.up * acceleration, ForceMode2D.Force);

        // Giro suave hacia el jugador
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.SignedAngle(transform.up, directionToPlayer);
        rb.MoveRotation(rb.rotation + angleToPlayer * directionAdjustSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.gameStarted) return;

        if (collision.CompareTag("Player"))
        {
            // Dañar al jugador
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(25f);
            }

            ExplodeAndDeactivate();
        }
        else if (collision.CompareTag("Danger"))
        {
            ExplosionManager.Instance.SpawnExplosion(collision.gameObject.transform.position);
            collision.gameObject.SetActive(false);
            ExplodeAndDeactivate();
        }
        else if (collision.CompareTag("Enemy"))
        {
            ExplodeAndDeactivate();
        }
    }

    private void ExplodeAndDeactivate()
    {
        ExplosionManager.Instance.SpawnExplosion(transform.position);
        gameObject.SetActive(false);
    }
}
