using UnityEngine;
using UnityEngine.UI;

public class BezierMovement : MonoBehaviour
{
    public Transform p1, p2, p3, p4;
    public Image progressImage;
    public Animator animator;
    public KeyCode resetPos;
    [Range(0f, 100f)]
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
        _bezierLength = CalculateBezierLength();
        _normalizedSpeed = speed / _bezierLength;
    }

    void FixedUpdate()
    {
        _normalizedSpeed = speed / _bezierLength;

        if (_t < 1f)
        {
            _t += Time.fixedDeltaTime * _normalizedSpeed;
            _t = Mathf.Clamp01(_t);

            Vector3 currentPos = CalculateBezierPoint(_t, p1.position, p2.position, p3.position, p4.position);
            transform.position = currentPos;

            Vector3 nextPos = CalculateBezierPoint(Mathf.Min(_t + Time.fixedDeltaTime * _normalizedSpeed, 1f), 
                                    p1.position, p2.position, p3.position, p4.position);
            _directionDefault = (nextPos - currentPos).normalized;

            progressImage.fillAmount = _t;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(resetPos))
        {
            ResetPosition();
        }

        if (_t < 1f)
        {
            animator.Play("zombie_walk");
        }
        else
        {
            animator.Play("zombie_idle");
            hasReachedDestination = true;
            _directionDefault = Vector3.zero;
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
