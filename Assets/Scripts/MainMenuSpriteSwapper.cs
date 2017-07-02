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

    // Use this for initialization
    public GameObject back;

    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;

    // Update is called once per frame
    void Update()
    {
        switch (_currentlySelectedLevelIndex)
        {
            case 1:
                back.GetComponent<Image>().sprite = one;
                break;
            case 2:
                back.GetComponent<Image>().sprite = two;
                break;
            case 3:
                back.GetComponent<Image>().sprite = three;
                break;
            case 4:
                back.GetComponent<Image>().sprite = four;
                break;
            case 5:
                back.GetComponent<Image>().sprite = five;
                break;

        }

    }

    public void LoadSelectedLevel()
    {
        FindObjectOfType<LevelManager>().LoadLevel(_currentlySelectedLevelIndex);
    }

    public void OnLevelSelectHoverEnter()
    {
        LevelSelectSelectedImage.GetComponent<Image>().enabled = true;
        LevelSelectionMenu.SetActive(true);
        GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
    }

    public void OnLevelSelectHoverExit()
    {
        LevelSelectSelectedImage.GetComponent<Image>().enabled = false;
    }

    public void OnLevelHoverEnter(int levelIndex)
    {
        GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
        _currentlySelectedLevelIndex = levelIndex;
    }

    public void OnStartHoverEnter()
    {
        StartSelectedImage.GetComponent<Image>().enabled = true;
        LevelSelectionMenu.SetActive(false);
        GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
    }

    public void OnStartHoverExit()
    {
        StartSelectedImage.GetComponent<Image>().enabled = false;
    }

    public void OnQuitHoverEnter()
    {
        QuitSelectedImage.GetComponent<Image>().enabled = true;
        GameManager.AudioManager.PlaySound(AudioManager.Sound.MenuInteraction);
    }

    public void OnQuitHoverExit()
    {
        QuitSelectedImage.GetComponent<Image>().enabled = false;
    }

}
