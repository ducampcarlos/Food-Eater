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
            AudioManager.Instance.PlayWrenchFix();
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            ExplosionManager.Instance.SpawnExplosion(collision.gameObject.transform.position);
            collision.gameObject.SetActive(false);
            GetComponent<PlayerHealth>().TakeDamage(25f);
            AudioManager.Instance.PlayBarrelBreak();
        }
        else if (collision.CompareTag("Objective"))
        {
            ScoreManager.Instance.AddScore(500); 
            ObjectiveManager.Instance.SpawnNewObjective();
            Destroy(collision.gameObject);
            AudioManager.Instance.PlayGrabMoney();
        }
    }
}
