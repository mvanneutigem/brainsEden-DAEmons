using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public CameraController Camera;
    static public AudioManager AudioManager;

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
            // TODO: Pause background music?
        }
    }
    
	void Start ()
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
    }
}
