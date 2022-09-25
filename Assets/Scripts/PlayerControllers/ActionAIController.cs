using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AIState
{

    IDLE,
    PATROLLING,
    CHASING,
    SHOOTING,

}

public class ActionAIController : MonoBehaviour
{

    [Header("AI State")]
    public AIState CurrentState = AIState.IDLE;

    [Header("Patrolling")]
    public float PatrolOffset = 50;
    private float _targetPatrolLocation;
    private float _targetPatrolLocationRight;
    private float _targetPatrolLocationLeft;

    [Header("Chasing")]
    public float chaseDurationMin = 1;
    public float chaseDurationMax = 3;
    public GameObject ChasingTarget;
    private float _chaseClock;
    private float _targetChaseTime;

    [Header("Attacking")]
    public float attackDurationMin = 1;
    public float attackDurationMax = 2;
    private float _attackClock;
    private float _targetAttackTime;

    private IActionPlayer _controlledPlayer;
    private PlayerDetection _detectionComponent;
    private IDamageable _damageable;

    // Start is called before the first frame update
    void Start()
    {
        
        _controlledPlayer = GetComponent<IActionPlayer>();
        _detectionComponent = GetComponentInChildren<PlayerDetection>();
        _damageable = GetComponent<IDamageable>();

        _targetPatrolLocationRight = gameObject.transform.position.x + PatrolOffset;
        _targetPatrolLocationLeft = gameObject.transform.position.x - PatrolOffset;
        _targetPatrolLocation = Random.Range(0, 1) == 1 ? _targetPatrolLocationLeft : _targetPatrolLocationRight;

        _damageable.OnAnyDamage += (_, DamageCauser) => {

            // If a player shoots an AI, jump the AI into attacking if he isn't already
            HumanPlayerController humanDamager = DamageCauser.GetComponent<HumanPlayerController>();
            if (humanDamager != null && (CurrentState != AIState.CHASING || CurrentState != AIState.SHOOTING))
            {

                ChasingTarget = DamageCauser;
                CurrentState = AIState.CHASING;

            }
        
        };

    }

    private bool TryGetEnemy(out GameObject enemy)
    {

        enemy = null;
        if (_detectionComponent == null) { return false; }
        
        var foundEnemies = _detectionComponent.GetPercievedObjects<HumanPlayerController>();
        if (foundEnemies.Count > 0) 
        { 
            
            enemy = foundEnemies.First().HitObject;
            return true;
        
        }

        return false;

    }

    private bool CheckForEnemiesAndAttack()
    {

        if (TryGetEnemy(out ChasingTarget))
        {

            CurrentState = AIState.CHASING;            
            return true;

        }

        return false;

    }

    void ToggleTarget()
    {
        
        _targetPatrolLocation =
            _targetPatrolLocation == _targetPatrolLocationLeft
                ? _targetPatrolLocationRight
                : _targetPatrolLocationLeft;

    }

    bool PassedTarget()
    {

        bool passedByLeft = _targetPatrolLocation == _targetPatrolLocationLeft && transform.position.x < _targetPatrolLocation;
        bool passedByRight = _targetPatrolLocation == _targetPatrolLocationRight && transform.position.x > _targetPatrolLocation;

        return passedByLeft || passedByRight;

    }

    void WalkTowardsPatrolTarget () 
    {

        if (PassedTarget()) { ToggleTarget(); }
        float stick = (transform.position.x > _targetPatrolLocation) ? -1 : 1;
        _controlledPlayer.Walk(stick);

    }

    // Update is called once per frame
    void Update()
    {

        switch (CurrentState)
        {

            case AIState.PATROLLING:

                // Look for enemies, patrol if there are none
                if (!CheckForEnemiesAndAttack()) { WalkTowardsPatrolTarget(); }
                break;

            case AIState.CHASING:

                // If the player has been killed or ran out of sight, drop out of this state
                if (ChasingTarget == null || !TryGetEnemy(out _))
                {

                    CurrentState = AIState.PATROLLING;
                    break;

                }

                bool moveLeft = ChasingTarget.transform.position.x < transform.position.x;
                float stick = moveLeft ? -1 : 1;
                _controlledPlayer.Walk(stick);

                // Switch out of chasing if we have been chasing for too long
                if ((_chaseClock += Time.deltaTime) >= _targetChaseTime)
                {

                    _targetAttackTime = Random.Range(attackDurationMin, attackDurationMax);
                    _attackClock = 0;
                    CurrentState = AIState.SHOOTING;

                }

                break;

            case AIState.SHOOTING:
                
                // If the player has been killed, drop out of this state
                if (ChasingTarget == null)
                {

                    CurrentState = AIState.PATROLLING;
                    break;

                }

                _controlledPlayer.Walk(0);
                _controlledPlayer.PrimaryAttack();

                // Switch out of shooting if we have been shooting for too long
                if ((_attackClock += Time.deltaTime) >= _targetAttackTime)
                {

                    _targetChaseTime = Random.Range(chaseDurationMin, chaseDurationMax);
                    _chaseClock = 0;
                    CurrentState = AIState.CHASING;

                }

                break;

            default:
                _controlledPlayer.Walk(0);
                break;

        }
        
    }
}
