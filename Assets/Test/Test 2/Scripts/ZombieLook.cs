using UnityEngine;
using UnityEngine.UI;

public class ZombieLook : MonoBehaviour
{
    public Transform[] targets;
    public float lookRange = 10f;
    public float lookAngle = 60f;
    public Image progressImage;
    private Transform nearestTarget;

    void Update()
    {
        nearestTarget = FindNearestTarget();

        if (nearestTarget != null)
        {
            LookAtTarget(nearestTarget);
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

    void LookAtTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }
}
