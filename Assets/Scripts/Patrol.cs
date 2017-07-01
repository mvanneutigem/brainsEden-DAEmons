using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform TransSelf;
    //private List<Vector3> _wayPoints = new List<Vector3>();
    public Transform[] Waypoints;
    public float Speed = 5f;
    public float CutCornerDistance = 5f;
    private bool _isDoingSmoothCorner = false;
    private int _currentWayPoint = 0;
    private int _previousWaypoint = 0;
    private Quaternion _lookRotation;
    private Vector3 _target;
    private Vector3 _direction;
    private bool _touched = false;
    public ParticleSystem ParticleSys;
    private float _cutCornerFactor = 0;
    public float MaxCutCornerFactor = 5;
    public float CornerIncrementSpeed = 0.1f;
    void Start()
    {
        _previousWaypoint = Waypoints.Length - 1;
        ParticleSys.Stop();
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
            int next = i + 1;
            if (next >= Waypoints.Length)
            {
                next = 0;
            }
            Debug.DrawLine(Waypoints[i].position, Waypoints[next].position,Color.black);
            
        }
        if (!_touched)
        {
            //if we are within cutcorner distance, start cutting corner
            if ((Waypoints[_currentWayPoint].position - transform.position).magnitude < CutCornerDistance)
            {
                _isDoingSmoothCorner = true;
                _cutCornerFactor = 0;
                if (Waypoints.Length > 0)
                    AddCurrentWaypoint();
            }
            //if we are on the line to the next point, stop cutting corner
            if ((Waypoints[_currentWayPoint].position - transform.position).magnitude + (Waypoints[_previousWaypoint].position - transform.position).magnitude == (Waypoints[_currentWayPoint].position - Waypoints[_previousWaypoint].position).magnitude)
                _isDoingSmoothCorner = false;
            if (_isDoingSmoothCorner)
            {
                if (_cutCornerFactor < MaxCutCornerFactor)
                {
                    _cutCornerFactor += CornerIncrementSpeed;
                }
                Vector3 smoothDirection = (Waypoints[_currentWayPoint].position - Waypoints[_previousWaypoint].position);
                smoothDirection.Normalize();
                _target = (Waypoints[_previousWaypoint].position + (smoothDirection * _cutCornerFactor));
                if ((_target - transform.position).magnitude < 0.1f)
                {
                    _target = Waypoints[_currentWayPoint].position;
                }
                _direction = (_target - transform.position).normalized;
                Debug.DrawLine(transform.position, _target, Color.red);
            }
            else
            {
                _target = Waypoints[_currentWayPoint].position;
                _direction = (_target - transform.position).normalized;
            }

            // Only turn if we aren't on top of the target point
            if (_direction.magnitude > 0.0f)
            {
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * Speed);
            }

            //waypoint reached, select next waypoint
            if (TransSelf.position.Equals(Waypoints[_currentWayPoint].position))
            {
                if (Waypoints.Length > 0)
                    AddCurrentWaypoint();
            }
            //Vector3 lerpValue = Vector3.Lerp(TransSelf.position + TransSelf.forward, Waypoints[_currentWayPoint].position, Time.deltaTime * 2);
            //TransSelf.LookAt(lerpValue/*Waypoints[_currentWayPoint].position*/);
            Debug.DrawLine(transform.position, _target, Color.green);
            TransSelf.position = Vector3.MoveTowards(TransSelf.position, _target, Speed * Time.deltaTime);
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
        ParticleSys.Play();
        FindObjectOfType<AudioManager>().PlaySound(AudioManager.Sound.HeadExplosion);
    }
}
