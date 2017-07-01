using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Intensity = 1.0f;
    public float Duration = 2.0f;

    private float _durationRemaining;
    private Vector3 _shakeOffset;

    private Camera _camera;
    private Vector3 _cameraStartingPosition;

    void Start ()
    {
        _camera = GetComponent<Camera>();
        _cameraStartingPosition = _camera.transform.position;
    }
	
	void Update ()
    {
		if (_durationRemaining > 0.0f)
        {
            _durationRemaining -= Time.deltaTime;
            _durationRemaining = Mathf.Max(0.0f, _durationRemaining);

            float durationScale = _durationRemaining / Duration;
            durationScale = Mathf.Clamp01(durationScale);

            _shakeOffset += 
                new Vector3(
                    Random.Range(-Intensity, Intensity), 
                    Random.Range(-Intensity, Intensity), 
                    0.0f) * 
                    durationScale;

            _camera.transform.position = _cameraStartingPosition + _shakeOffset;
        }
    }

    public void Shake()
    {
        _durationRemaining = Duration;
        _shakeOffset = Vector3.zero;
    }
}
