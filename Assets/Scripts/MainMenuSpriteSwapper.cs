using UnityEngine;
using UnityEngine.UI;

public class MainMenuSpriteSwapper : MonoBehaviour
{
    public Image BaseImage;
    public Image LevelSelectSelectedImage;
    public Image StartSelectedImage;
    public Image QuitSelectedImage;
    public GameObject LevelSelectionMenu;

    private int _currentlySelectedLevelIndex = 1;

    public void LoadSelectedLevel()
    {
        FindObjectOfType<LevelManager>().LoadLevel(_currentlySelectedLevelIndex);
    }

    public void OnLevelSelectHoverEnter()
    {
        LevelSelectSelectedImage.GetComponent<Image>().enabled = true;
        LevelSelectionMenu.SetActive(true);
    }

    public void OnLevelSelectHoverExit()
    {
        LevelSelectSelectedImage.GetComponent<Image>().enabled = false;
    }

    public void OnLevelHoverEnter(int levelIndex)
    {
        // TODO: Show blurred screenshot of each level here
    }

    public void OnStartHoverEnter()
    {
        StartSelectedImage.GetComponent<Image>().enabled = true;
        LevelSelectionMenu.SetActive(false);
    }

    public void OnStartHoverExit()
    {
        StartSelectedImage.GetComponent<Image>().enabled = false;
    }

    public void OnQuitHoverEnter()
    {
        QuitSelectedImage.GetComponent<Image>().enabled = true;
    }

    public void OnQuitHoverExit()
    {
        QuitSelectedImage.GetComponent<Image>().enabled = false;
    }

}
