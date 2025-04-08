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
        // Aquí puedes agregar lógica para mostrar la pantalla de Game Over
    }
}
