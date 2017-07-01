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
    }

    public void OnResetSelect(bool selected)
    {
        ResetHighlightImage.enabled = selected;
    }

    public void OnMainMenuSelect(bool selected)
    {
        MainMenuHighlightImage.enabled = selected;
    }

    public void OnQuitSelect(bool selected)
    {
        QuitHighlightImage.enabled = selected;
    }
}
