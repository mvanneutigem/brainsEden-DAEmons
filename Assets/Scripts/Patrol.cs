using System;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Serializable]
    public class Waypoint
    {
        public Transform Transform;
        public float pauzeTime = 0;
        public Actions pauzeType;
    }

    public enum Actions
    {
        Freeze = 0,
        Rotate = 1
    }

    //private List<Vector3> _wayPoints = new List<Vector3>();
    public Waypoint[] Waypoints;
    public float Speed = 5f;
    public float CutCornerDistance = 5f;
    public int _nextWayPoint = 0;
    private bool _isDoingSmoothCorner = false;
    private int _currentWayPoint = 0;
    private int _previousWaypoint = 0;
    private Quaternion _lookRotation;
    private Vector3 _target;
    private Vector3 _direction;
    private bool _touched = false;

    private float _cutCornerFactor = 0;
    public float MaxCutCornerFactor = 5;
    public float CornerIncrementSpeed = 0.1f;
    private bool _hasSneezed = false;
    private Sneeze[] _sneezes;
    private float _timePauzed = 0;
    private bool _shouldMove = true;
    //avoidance
    private GameObject Player;
    public float _DetectionRadius = 2.5f;
    public float _LookAtPlayerRotationSpeedMultiplier = 2;

    void Awake ()
    {
        _sneezes = GetComponentsInChildren<Sneeze>();
    }
    void Start()
    {
        _previousWaypoint = Waypoints.Length - 1;
        Player = GameObject.Find("Player");
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

    void Update()
    {
        for (int i = 0; i < Waypoints.Length; i++)
        {
            if (!_touched)
            {
                int next = i + 1;
                if (next >= Waypoints.Length)
                {
                    next = 0;
                }
                Debug.DrawLine(Waypoints[i].Transform.position, Waypoints[next].Transform.position, Color.black);
            }
        }

        if (!_touched)
        {
            _shouldMove = true;
            //*************
            //avoidance
            //*************
            checkIfWeShouldAvoidPlayer();
            if (!_shouldMove)
                lookTowardsPlayer();

            //*************
            //movement and looking
            //*************
            if (_shouldMove)
            {
                CheckIfWeShouldCutCorner();
                CalculateTargetAndDirection();
                //this check shouldn't be necessary anymore but w/e, gamejam code
                // Only turn if we aren't on top of the target point
                if (_direction.magnitude > 0.0f)
                {
                    _lookRotation = Quaternion.LookRotation(_direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * Speed);
                }

                //*************
                //waypoint actions
                //*************
                //waypoint reached, do waypoint action and select next waypoint
                if (transform.position.Equals(Waypoints[_currentWayPoint].Transform.position))
                {
                    _timePauzed += Time.deltaTime;
                    switch (Waypoints[_currentWayPoint].pauzeType)
                    {
                        case Actions.Freeze:
                            break;
                        case Actions.Rotate:
                            //getcomponent -> startrotate?
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    if (_timePauzed < Waypoints[_currentWayPoint].pauzeTime)
                        return;
                    if (Waypoints.Length > 0)
                    {
                        _timePauzed = 0;
                        AddCurrentWaypoint();
                    }
                }
                
                Debug.DrawLine(transform.position, _target, Color.green);
                transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
            }
        }
    }

    void AddCurrentWaypoint()
    {
        ++_currentWayPoint;
        if (_currentWayPoint >= Waypoints.Length)
        {
            _currentWayPoint = 0;
        }
        _previousWaypoint = _currentWayPoint - 1;
        if (_currentWayPoint == 0)
        {
            _previousWaypoint = Waypoints.Length - 1;
        }
    }

    void Sneeze()
    {
        if (_hasSneezed) return;
        _hasSneezed = true;
        
        foreach (var sneeze in _sneezes)
        {
            sneeze.Play();
        }
        
        GameManager.Camera.Shake();
        GameManager.AudioManager.PlaySound(AudioManager.Sound.HeadExplosion);
    }

    void CheckIfWeShouldCutCorner()
    {
        //if we are within cutcorner distance, start cutting corner
        if (Waypoints[_currentWayPoint].pauzeTime == 0 && ((Waypoints[_currentWayPoint].Transform.position - transform.position).magnitude < CutCornerDistance))
        {
            _isDoingSmoothCorner = true;
            _cutCornerFactor = 0;
            if (Waypoints.Length > 0)
                AddCurrentWaypoint();
        }
        //if we are on the line to the next point, stop cutting corner
        if ((Waypoints[_currentWayPoint].Transform.position - transform.position).magnitude + (Waypoints[_previousWaypoint].Transform.position - transform.position).magnitude == (Waypoints[_currentWayPoint].Transform.position - Waypoints[_previousWaypoint].Transform.position).magnitude)
            _isDoingSmoothCorner = false;
    }

    void CalculateTargetAndDirection()
    {
        if (_isDoingSmoothCorner)
        {
            if (_cutCornerFactor < MaxCutCornerFactor)
            {
                _cutCornerFactor += CornerIncrementSpeed;
            }
            Vector3 smoothDirection = (Waypoints[_currentWayPoint].Transform.position - Waypoints[_previousWaypoint].Transform.position);
            smoothDirection.Normalize();
            _target = (Waypoints[_previousWaypoint].Transform.position + (smoothDirection * _cutCornerFactor));

            //if we're too close to the target to move closer, but not close enough to be ON the line, move on
            if ((_target - transform.position).magnitude < 0.1f)
            {
                _target = Waypoints[_currentWayPoint].Transform.position;
                _isDoingSmoothCorner = false;
            }

            _direction = (_target - transform.position).normalized;
            Debug.DrawLine(transform.position, _target, Color.red);
        }
        else
        {
            _target = Waypoints[_currentWayPoint].Transform.position;
            _direction = (_target - transform.position).normalized;
        }
    }

    void checkIfWeShouldAvoidPlayer()
    {
        if ((Player.transform.position - transform.position).magnitude < _DetectionRadius)
        {
            _shouldMove = false;
        }
    }

    void lookTowardsPlayer()
    {
        var lookPos = Player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _LookAtPlayerRotationSpeedMultiplier);

        /*Vector3 lerpValue = Vector3.Lerp(transform.position + transform.forward, Player.transform.position, Time.deltaTime * _LookAtPlayerRotationSpeedMultiplier);
        //lerpValue.x = 0;
        //lerpValue.z = 0;
        Debug.Log("looking at player");
        //transform.LookAt(lerpValue);*/
    }
}