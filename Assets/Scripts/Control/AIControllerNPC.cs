using RPG.Core;
using RPG.Movement;
using RPG.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIControllerNPC : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 2f;
        [SerializeField] float lookAtOffset = 0.15f;
        [SerializeField] PatrolPath patrolPath;

        LazyValue<Vector3> guardPosition;
        GameObject player;

        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");

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
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 3)
            {
                PlayerNear();
            }
            else
            {
                PatrolBehaviour();
            }
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

        private void PlayerNear()
        {
            timeSinceArrivedAtWaypoint = 0;
            Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + lookAtOffset, player.transform.position.z);
            transform.LookAt(playerPos);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}