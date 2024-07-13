using UnityEngine;

public class ZombieLookWithBody : MonoBehaviour
{
    public BezierMovement bezierMovement;
    public Transform[] points;            // Array of points to look at
    public Transform rigMain;             // Rotate the whole body
    public Transform target;              // The goal is to let your head and chest rotate as you move
    public float lookRange = 10f;         // Maximum range to look for targets
    public float lookAngle = 60f;         // Maximum angle to look for targets
    private Transform _nextNearestTarget; // The next nearest target
    private Vector3 _offset;
    private Vector3 _next;

    private void Start()
    {
        // The purpose is so that the zombie's head does not have to look up
        _offset = new Vector3(0, 0.2f, 0);
    }


    void Update()
    {
        _nextNearestTarget = FindNearestTarget();

        // If a nearest target is found, interpolate towards its position
        if (_nextNearestTarget != null)
        {
            _next = _nextNearestTarget.position - _offset;
            target.position = Vector3.Lerp(target.position, _next, Time.deltaTime);
        }
        else
        {
            target.position = Vector3.Lerp(target.position, bezierMovement.GetBezierDirection(), Time.deltaTime);
            Debug.Log("111111");
        }

        // Rotate the rig towards the direction of the Bezier movement
        Quaternion lookRotation = bezierMovement.hasReachedDestination
            ? Quaternion.Euler(0, 0, 0)
            : Quaternion.LookRotation(bezierMovement.GetBezierDirection());

        rigMain.rotation = Quaternion.RotateTowards(rigMain.rotation, lookRotation, Time.deltaTime * 100f);
    }

    Transform FindNearestTarget()
    {
        Transform nearest = null;
        float nearestDistance = float.MaxValue;

        // Iterate through all points to find the nearest one within range and angle
        foreach (var point in points)
        {
            float distance = Vector3.Distance(transform.position, point.position);
            Vector3 directionToTarget = point.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (distance < lookRange && angle < lookAngle && distance < nearestDistance)
            {
                nearest = point;
                nearestDistance = distance;
            }
        }

        return nearest;
    }
}
