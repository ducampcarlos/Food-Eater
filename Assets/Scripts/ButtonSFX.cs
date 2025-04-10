using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    //Adds Listener, when is pressed, the sfx is played
    private void Start()
    {
        var button = GetComponent<UnityEngine.UI.Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSFX);
        }
    }

    public void PlayButtonSFX()
    {
        AudioManager.Instance.PlayButton();
    }
}
