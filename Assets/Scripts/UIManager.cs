using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject playButton;

    private void Awake()
    {
        Instance = this;
    }

    public void HidePlayButton()
    {
        playButton.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        playButton.SetActive(true);
        // Aqu� puedes agregar l�gica para mostrar la pantalla de Game Over
    }
}
