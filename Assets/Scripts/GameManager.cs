using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool _paused;
    public bool Paused
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
        if (!FindObjectOfType<AudioManager>())
        {
            Debug.LogError("You need to add an audio manager to the scene! (prefab)");
        }
    }
	
	void Update ()
    {
		
	}
}
