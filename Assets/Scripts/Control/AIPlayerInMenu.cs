using RPG.Movement;
using RPG.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIPlayerInMenu : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 2f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] int endIndex = 0;

        bool sitDown = false;

        LazyValue<Vector3> guardPosition;
        Animator animator;

        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
            guardPosition.ForceInit();
        }

        public void Reset()
        {
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.Warp(guardPosition.value);
            timeSinceArrivedAtWaypoint = Mathf.Infinity;
            currentWaypointIndex = 0;
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Update()
        {
            if (currentWaypointIndex == endIndex && !sitDown)
            {
                SitDownAnim();
            }
            if (sitDown) return;
            PatrolBehaviour();
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                GetComponent<Mover>().StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SitDownAnim()
        {
            sitDown = true;
            animator.SetTrigger("sitdown");
            animator.SetBool("sitidle", true);
        }

        public IEnumerator StandupAnim()
        {
            if (!sitDown) yield break;
            animator.SetBool("sitidle", false);
            animator.SetTrigger("standup");
            yield return new WaitForSeconds(3);
            endIndex++;
            sitDown = false;          
        }

        public void StandupRoutine()
        {
            StartCoroutine(StandupAnim());
        }
    }
}