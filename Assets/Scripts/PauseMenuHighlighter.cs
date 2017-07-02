using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHighlighter : MonoBehaviour
{
    public Image ResumeHighlightImage;
    public Image ResetHighlightImage;
    public Image MainMenuHighlightImage;
    public Image QuitHighlightImage;

    public void OnResumeSelect(bool selected)
    {
        ResumeHighlightImage.enabled = selected;
        if (selected)
        {
            GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
        }
    }

    public void OnResetSelect(bool selected)
    {
        ResetHighlightImage.enabled = selected;
        if (selected)
        {
            GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
        }
    }

    public void OnMainMenuSelect(bool selected)
    {
        MainMenuHighlightImage.enabled = selected;
        if (selected)
        {
            GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
        }
    }

    public void OnQuitSelect(bool selected)
    {
        QuitHighlightImage.enabled = selected;
        if (selected)
        {
            GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
        }
    }

    public void OnSliderValueChange()
    {
        GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
    }

    public void OnSliderSelected(bool selected)
    {
        if (selected)
        {
            GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
        }
    }
}
