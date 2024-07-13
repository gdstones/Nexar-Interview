using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, -2.4f);
    public float followSpeed = 10f;
    public float rotationSpeed = 10f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + target.rotation * offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = smoothedRotation;
        }
    }
}
