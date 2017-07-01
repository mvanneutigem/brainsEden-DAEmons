using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private Canvas _mainCanvas;

	void Start ()
    {
        _mainCanvas = GetComponent<Canvas>();
        if (!_mainCanvas)
        {
            Debug.LogError("Pause Menu doesn't have canvas component!");
        }

    }
	
	void Update ()
    {
		
	}
}
