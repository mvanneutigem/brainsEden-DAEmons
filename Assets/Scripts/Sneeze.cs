using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour
{
	public ParticleSystem Sys;

	public float stopOffset = 1;

	void Awake ()
	{
		Sys = GetComponent<ParticleSystem>();
	}
	
	void Start()
	{
		Sys.Stop();
	}

	void Update () 
	{
		if (Sys.time + stopOffset >= Sys.main.duration)
		{
			Sys.Stop();
		}
	}

	public void Play()
	{
		Sys.Clear();
		Sys.time = 0;
		Sys.Play();
	}
}
