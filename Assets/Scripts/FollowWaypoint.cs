using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 3.0f;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        /* Check if we have reached the final waypoint
         * And reset index if so.
         */
        if (currentWaypointIndex >= waypoints.Length)
            currentWaypointIndex = 0;

        // Get the current target waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Rotate(targetPosition);
        CheckDistance(targetPosition);
    }

    private void Rotate(Vector3 targetPosition)
    {
        /* Rotate towards the waypoint
         * Normalized because longer distances would mean faster movement if the time is the same
         */
        Vector3 directionToWaypoint = (targetPosition - transform.position).normalized;
        if (directionToWaypoint != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToWaypoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckDistance(Vector3 targetPosition)
    {
        // Check if we've reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}
