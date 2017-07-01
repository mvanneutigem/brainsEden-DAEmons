using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static CameraController Camera;
    public static AudioManager AudioManager;
    public static PauseMenu PauseMenu;
    private static Canvas _pauseMenuCanvas;

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

            // TODO: Pause background music?
        }
    }

    // Non-static function for menu buttons
    public void SetPaused(bool paused)
    {
        Paused = paused;
    }
    
	void Start()
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
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Paused = !Paused;
            Debug.Log(Paused);
        }
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
