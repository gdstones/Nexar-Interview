using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BezierMovement : MonoBehaviour
{
    public Transform p1, p2, p3, p4;
    [Range(1f, 30f)]  public float speed;
    private float t = 0f;
    public Image progressImage;

    private Vector3 _startPos;
    private float _timeCoefficient;

    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        _timeCoefficient = speed / 30f;

        if (t < 1f)
        {
            t += Time.deltaTime * _timeCoefficient;
            transform.position = CalculateBezierPoint(t, p1.position, p2.position, p3.position, p4.position);
            progressImage.fillAmount = t; // assuming the progressImage is of type Image and using fillAmount to show progress
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.position = _startPos;
        t = 0;
    }

    //https://vi.wikipedia.org/wiki/%C4%90%C6%B0%E1%BB%9Dng_cong_B%C3%A9zier
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
