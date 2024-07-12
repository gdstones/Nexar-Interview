using UnityEngine;

public class ZombieLookWithBody : MonoBehaviour
{
    public Transform[] targets;
    public float lookRange = 10f;
    public float lookAngle = 60f;
    public Transform head;
    public Transform body;
    private Transform _nearestTarget;
    public bool allowBodyRotation = true; 

    void Update()
    {
        _nearestTarget = FindNearestTarget();

        if (_nearestTarget != null)
        {
            LookAtHead(_nearestTarget);

            if (allowBodyRotation)
            {
                LookAtBody(_nearestTarget);
            }
        }
    }

    Transform FindNearestTarget()
    {
        Transform nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            Vector3 directionToTarget = target.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (distance < lookRange && angle < lookAngle / 2 && distance < nearestDistance)
            {
                nearest = target;
                nearestDistance = distance;
            }
        }

        return nearest;
    }

    void LookAtHead(Transform target)
    {
        Vector3 direction = target.position - head.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        head.rotation = Quaternion.Slerp(head.rotation, lookRotation, Time.deltaTime * 2f);
    }

    void LookAtBody(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Quaternion bodyRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        body.rotation = Quaternion.Slerp(body.rotation, bodyRotation, Time.deltaTime * 2f);
    }
}
