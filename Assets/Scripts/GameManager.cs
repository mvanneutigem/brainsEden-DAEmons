using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static CameraController Camera;
    public static AudioManager AudioManager;
    public static PauseMenu PauseMenu;
    private static Canvas _pauseMenuCanvas;

    private static GameObject _resumeButton;

    public static bool IsSneezing;

    static private bool _paused;
    static public bool Paused
    {
        get
        {
            return _paused;
        }
        set
        {
            _paused = value;
            _pauseMenuCanvas.enabled = _paused;

            if (_paused)
            {
                FindObjectOfType<EventSystem>().SetSelectedGameObject(_resumeButton);
            }
            else
            {
                FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
            }
            // TODO: Pause background music?
        }
    }

    // Non-static function for menu buttons
    public void SetPaused(bool paused)
    {
        Paused = paused;
    }

    public enum GameScene
    {
        MainMenu = 0,
        LevelSelection,
        Game // TODO: Have one for each level?
    }

    public static GameScene CurrentGameScene;

    public static List<Sneeze> _sneezes;
    public static PlayerController _playerController;


    void Awake()
    {
        Camera = FindObjectOfType<CameraController>();
        if (!Camera)
        {
            Debug.LogError("No camera controller component in scene! (use prefab)");
        }

        AudioManager = FindObjectOfType<AudioManager>();
        if (!AudioManager)
        {
            Debug.LogError("No audio manager in scene! (use prefab)");
        }

        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        if (!PauseMenu)
        {
            Debug.LogError("Pause menu not found! (use prefab)");
        }

        _pauseMenuCanvas = PauseMenu.GetComponent<Canvas>();
        if (!_pauseMenuCanvas)
        {
            Debug.LogError("Pause menu doesn't have a canvas component!");
        }

        _resumeButton = GameObject.Find("ResumeButton").gameObject;

        LoadData();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Paused = !Paused;
            Debug.Log(Paused);
        }

        if (IsSneezing)
        {
            bool isSneezing = false;
            bool doneSneezing = true;

            foreach (var sneeze in _sneezes)
            {
                if (sneeze.IsSneezing())
                {
                    isSneezing = true;
                    break;
                }

                if (!sneeze.HasSneezed() && doneSneezing)
                {
                    doneSneezing = false;
                }
            }

            if (isSneezing)
            {
                print("Still sneezing");
            }
            else
            {
                if (doneSneezing)
                {
                    print("everybody sneezed");
                }
                else
                {
                    print("not everybody sneezed");
                }
            }
        }
    }

    public void LoadData()
    {
        _sneezes = new List<Sneeze>();
        _sneezes.AddRange(FindObjectsOfType<Sneeze>());
    }


    // Non-static function for menu buttons
    public void CallQuit()
    {
        Quit();
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
