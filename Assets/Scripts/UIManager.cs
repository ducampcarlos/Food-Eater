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
}
