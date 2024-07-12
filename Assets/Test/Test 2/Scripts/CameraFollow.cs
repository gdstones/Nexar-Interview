using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset = new Vector3(0, 1, -2.4f);  
    public float followSpeed = 10f;

    void Start()
    {
        transform.position = target.position + offset;
    }


    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            transform.LookAt(target);  
        }
    }
}
