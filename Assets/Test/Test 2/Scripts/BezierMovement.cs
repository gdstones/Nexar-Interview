using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BezierMovement : MonoBehaviour
{
    public Transform p1, p2, p3, p4;
    [Range(1f, 30f)]  public float speed;
    private float _t = 0f;
    public Image progressImage;
    public Animator _animator;

    private Vector3 _startPos;
    private float _timeCoefficient;

    private void Start()
    {
        _startPos = transform.position;
        _animator.Play("zombie_idle");
    }

    void Update()
    {
        _timeCoefficient = speed / 30f;

        if (_t < 1f)
        {
            _animator.Play("zombie_walk");
            _t += Time.deltaTime * _timeCoefficient;
            transform.position = CalculateBezierPoint(_t, p1.position, p2.position, p3.position, p4.position);
            progressImage.fillAmount = _t; // assuming the progressImage is of type Image and using fillAmount to show progress
        }
        else
        {
            _animator.Play("zombie_idle");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.position = _startPos;
        _t = 0;
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
}
