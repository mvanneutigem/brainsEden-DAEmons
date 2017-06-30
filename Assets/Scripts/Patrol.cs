﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patrol : MonoBehaviour
{
    public Transform TransSelf;
    //private List<Vector3> _wayPoints = new List<Vector3>();
    public Transform[] Waypoints;
    public float Speed = 5f;
    public int _currentWayPoint = 0;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private bool _touched = false;
    public GameObject ParticlesSystemGameObject;

    // Use this for initialization
    void Start()
    {
        ParticlesSystemGameObject.GetComponent<ParticleSystem>().Stop();
        // _wayPoints.Add(Vector3.zero);
        //for(int i = 0; i < Waypoints.Length)
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _touched = true;
            Debug.Log("Don't touch me I'm Scared");
            Sneeze();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        _touched = true;
        Debug.Log("ChainSneeze activated");
        Sneeze();
    }

    // Update is called once per frame
        void Update()
    {
        if (!_touched)
        {
            _direction = (Waypoints[_currentWayPoint].position - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * Speed);
            //waypoint reached, select next waypoint
            if (TransSelf.position.Equals(Waypoints[_currentWayPoint].position))
            {
                if (Waypoints.Length > 0)
                    AddCurrentWaypoint();
            }
            //Vector3 lerpValue = Vector3.Lerp(TransSelf.position + TransSelf.forward, Waypoints[_currentWayPoint].position, Time.deltaTime * 2);
            //TransSelf.LookAt(lerpValue/*Waypoints[_currentWayPoint].position*/);
            TransSelf.position = Vector3.MoveTowards(TransSelf.position, Waypoints[_currentWayPoint].position, Speed * Time.deltaTime);
        }
    }

    void AddCurrentWaypoint()
    {
        ++_currentWayPoint;
        if (_currentWayPoint >= Waypoints.Length)
        {
            _currentWayPoint = 0;
        }
    }

    void Sneeze()
    {
        ParticlesSystemGameObject.GetComponent<ParticleSystem>().Play();
    }
}
