using UnityEngine;
using UnityEngine.UI;

public class ObjectiveArrow : MonoBehaviour
{
    public Transform arrowTransform; // El transform de la flecha
    private Transform player;
    public Transform target;
    public float distanceFromPlayer = 2f; // En unidades del mundo
    public float hideIfCloserThan = 3f;

    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    void Update()
    {
        if (player == null || target == null)
        {
            GetComponent<Image>().enabled = false;
            return;
        }

        Vector3 toTarget = target.position - player.position;
        float distance = toTarget.magnitude;

        if (distance < hideIfCloserThan)
        {
            GetComponent<Image>().enabled = false;
            return;
        }

        GetComponent<Image>().enabled = true;

        // Posición de la flecha alrededor del player
        Vector3 dir = toTarget.normalized;
        arrowTransform.position = player.position + dir * distanceFromPlayer;

        // Rotación para mirar al objetivo
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrowTransform.rotation = Quaternion.Euler(0, 0, angle - 90f); // flecha apunta hacia arriba
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
