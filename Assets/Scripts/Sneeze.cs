using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour
{
	private ParticleSystem _sys;

	private float _stopOffset = .25f;
	public List<Color> Colors;
	public static int _colorIndex = -1;

	void Awake ()
	{
		_sys = GetComponent<ParticleSystem>();
		if (_colorIndex == -1)
		{
			_colorIndex = Random.Range(0, Colors.Count / 2) * 2;
			print(_colorIndex);
		}
	}
	
	void Start()
	{
		_sys.Stop();
	}

	void Update () 
	{
		if (_sys.time + _stopOffset >= _sys.main.duration)
		{
			_sys.Pause();
		}
	}

	public void Play()
	{
		_sys.Clear();
		_sys.time = 0;
		var main = _sys.main;

		main.startColor = new ParticleSystem.MinMaxGradient(Colors[_colorIndex], Colors[_colorIndex + 1]);
		_colorIndex += 2;
		_colorIndex %= Colors.Count;

		_sys.Play();
	}
}
