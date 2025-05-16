using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopDistance = 0.3f; // Distance at which to switch directions

    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 target;

    private void Start()
    {
        // Store the start and end positions
        startPoint = pointA.position;
        endPoint = pointB.position;
        target = endPoint;
    }

    private void Update()
    {
        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if any part of the ship is close enough to the target
        if (Vector3.Distance(GetComponent<Collider>().ClosestPoint(target), target) <= stopDistance)
        {
            // Switch the target
            target = (target == endPoint) ? startPoint : endPoint;
        }
    }
}
