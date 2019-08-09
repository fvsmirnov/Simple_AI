using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;


public class BaseBrain : MonoBehaviour
{
    [Header("Objects")]
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public VisionBase vision;
    [Header("Available states")]
    public BaseState[] states;
    public bool changeStatePossible = true;
    [Header("Partrol waypoints")]
    public Transform[] patrolPoints;
    [HideInInspector] public int nextPatrolPoint; 
    [Header("Other pref")]
    public float normalSpeed = 1.5f;
    public float chaseSpeed = 2f;
    public bool useAI = true;

    [SerializeField] private BaseState currentState;
    private GameObject target;
    private Vector3 lastTargetPosition;
    private Dictionary<Type, BaseState> _states = new Dictionary<Type, BaseState>();

    public BaseState CurrentState
    {
        get { return currentState; }
        private set { currentState = value; }
    }

    public virtual GameObject Target
    {
        get { return target; }
        set
        {
            if (target == null && value != null)
            {
                //navMeshAgent.isStopped = false;
                lastTargetPosition = value.transform.position;
                ChangeState(typeof(BaseChaseState));
            }
            else if (target != null && value == null)
            {
                //navMeshAgent.isStopped = true;
                navMeshAgent.SetDestination(lastTargetPosition); // go to last known pos
                ChangeState(typeof(BaseTargetLostState));
            }
            target = value;
        }
    }

    public void ChangeState(Type stateType)
    {
        if (!changeStatePossible)
            return;

        if (CurrentState == null)
        {
            if (_states[stateType] != null)
            {
                CurrentState = _states[stateType];
                CurrentState.Init(this);    //Call new state start 
            }
        }
        else
        {
            if (CurrentState.GetType().BaseType != stateType)
            {

                if (_states[stateType] != null)                
                {
                    CurrentState.Exit(this);    //Call current state exit
                    CurrentState = _states[stateType];
                    CurrentState.Init(this);    //Call new state start 
                }
            }   
        }
    }

    public void SetNavMeshSpeed(float value)
    {
        navMeshAgent.speed = value;
    }

    public void ResetAnimatorState()
    {
        animator.SetBool("PatrolB", false);
        animator.SetBool("PrepareAttackB", false);
        animator.SetBool("TargetLostB", false);
        animator.SetBool("AttackB", false);
    }

    //Protected methods
    protected virtual void Start()
    {
        InitStates();

        if (useAI)
        {
            InitStartPoint();
            animator.speed = navMeshAgent.speed;
            ChangeState(typeof(BaseWanderState));   //Init start state
        }
    }

    protected virtual void InitStates()
    {
        for (int i = 0; i < states.Length; i++)
        {
            _states.Add(states[i].GetType().BaseType, states[i]);
        }
        states = null; //Prepare to destroy unused array
    }

    //Set start patrol point next after nearest.
    void InitStartPoint()
    {
        float nearestDist = int.MaxValue;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float nextDist = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (nextDist < nearestDist)
            {
                nextPatrolPoint = i + 1;
                nearestDist = nextDist;
            }
        }

        nextPatrolPoint = nextPatrolPoint % patrolPoints.Length;
    }

    private float lookWeight = 0;
    private float lookSmoother = 3;
    protected virtual void Update()
    {
        if (useAI)
        {
            //Check target
            Target = vision.Detect();
            if (target != null)
            {
                //IK
                lookWeight = Mathf.Lerp(lookWeight, 1f, Time.deltaTime * lookSmoother);
                SetNavMeshSpeed(Mathf.Lerp(navMeshAgent.speed, chaseSpeed, Time.deltaTime * lookSmoother));
            }
            else
            {
                //IK
                lookWeight = Mathf.Lerp(lookWeight, 0f, Time.deltaTime * lookSmoother);
                SetNavMeshSpeed(Mathf.Lerp(navMeshAgent.speed, normalSpeed, Time.deltaTime * lookSmoother));
            }

            animator.SetFloat("MoveSpeed", navMeshAgent.speed); //Change animation speed
        }

        CurrentState?.Execute(this);
    }

    //Private methods
    private void OnAnimatorIK(int layerIndex)
    {
        if (target != null)
        {
            animator.SetLookAtWeight(lookWeight);
            animator.SetLookAtPosition(target.transform.position);
        }
    }
}