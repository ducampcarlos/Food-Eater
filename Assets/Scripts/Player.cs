using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            collision.gameObject.SetActive(false);
            GetComponent<PlayerHealth>().Heal(25f);
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            collision.gameObject.SetActive(false);
            GetComponent<PlayerHealth>().TakeDamage(25f);
        }
    }
}
