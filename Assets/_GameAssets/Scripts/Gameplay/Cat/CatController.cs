using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;

    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;
    [SerializeField] private float _chaseSpeed = 7f;

    [Header("Navigaiton Settings")]
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private int _maxDestinationAttempts = 10;
    [SerializeField] private float _chaseDistanceThreshold = 1.5f;
    [SerializeField] private float _chaseDistance = 2f;

    private NavMeshAgent _catAgent;
    private CatStateController _catStateController;
    private Vector3 _initialPosition;
    private bool _isWaiting;
    private bool _isChasing;
    private float _timer;


    void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();
    }
    void Start()
    {
        _initialPosition = transform.position;
        SetRandomDestination();
    }
    void Update()
    {
        if (_playerController.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }
        
    }
    public void SetChaseMovement()
    {
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offPosition = _playerTransform.position - directionToPlayer*_chaseDistanceThreshold;
        _catAgent.SetDestination(offPosition);
        _catAgent.speed = _chaseSpeed;
        _catStateController.ChangeState(CatState.Running);

        if(Vector3.Distance(transform.position, _playerTransform.position)<= _chaseDistance && _isChasing)
        {
            _catStateController.ChangeState(CatState.Attacking);
            _isChasing = false;
        } 
    }
    public void SetPatrolMovement()
    {
        _catAgent.speed = _defaultSpeed;
        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)// Meali = kedi gideceği yere vardı mı?
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }
        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }
    }
    public void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;

        while (attempts < _maxDestinationAttempts && !destinationSet)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;
            randomDirection += _initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                if (!IsPositionBlocked(finalPosition))
                {
                    _catAgent.SetDestination(finalPosition);
                    destinationSet = true;
                }
                else { attempts++; }
            }
            else { attempts++; }
        }
        if (!destinationSet)
        {
            Debug.LogWarning("Failed to find a valid destination");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }
    }
    private bool IsPositionBlocked(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }
}
