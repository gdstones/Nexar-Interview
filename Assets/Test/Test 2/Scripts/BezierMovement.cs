using UnityEngine;
using UnityEngine.UI;

public class BezierMovement : MonoBehaviour
{
    public Transform p1, p2, p3, p4;
    public Image progressImage;
    public Animator animator;
    [Range(0.1f, 100f)]
    public float speed;
    public bool hasReachedDestination;

    private float _t = 0f;
    private Vector3 _startPos;
    private Vector3 _directionDefault;

    private float _bezierLength;
    private float _normalizedSpeed;

    private void Start()
    {
        animator.Play("zombie_idle");
        _startPos = transform.position;
        _bezierLength = CalculateBezierLength(200);
        _normalizedSpeed = speed / _bezierLength;
    }

    void Update()
    {
        _normalizedSpeed = speed / _bezierLength;

        if (_t < 1f)
        {
            animator.Play("zombie_walk");
            _t += Time.deltaTime * _normalizedSpeed;
            _t = Mathf.Clamp01(_t);  // Ensure _t doesn't exceed 1

            Vector3 currentPos = CalculateBezierPoint(_t, p1.position, p2.position, p3.position, p4.position);
            transform.position = currentPos;

            Vector3 nextPos = CalculateBezierPoint(Mathf.Min(_t + Time.deltaTime * _normalizedSpeed, 1f),
                                p1.position, p2.position, p3.position, p4.position);
            _directionDefault = (nextPos - currentPos).normalized;

            progressImage.fillAmount = _t;
        }
        else
        {
            animator.Play("zombie_idle");
            hasReachedDestination = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        _t = 0;
        transform.position = _startPos;
        hasReachedDestination = false;
    }

    // https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    float CalculateBezierLength(int segments = 100)
    {
        float length = 0f;
        Vector3 previousPoint = CalculateBezierPoint(0, p1.position, p2.position, p3.position, p4.position);

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 currentPoint = CalculateBezierPoint(t, p1.position, p2.position, p3.position, p4.position);
            
            length += Vector3.Distance(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        return length;
    }

    public Vector3 GetBezierDirection()
    {
        return _directionDefault;
    }
}
