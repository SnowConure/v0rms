using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcRenderer : MonoBehaviour
{
    private LineRenderer line;
    [Range(2, 30)]
    public int resolution;

    public Vector2 velocity;
    public float yLimit;
    private float g;

    [Range(2, 30)]
    public int linecastResolution;
    public LayerMask canHit;

    private Vector3 lastPos;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    public void ClearArc()
    {
        line.positionCount = 0;
    }
    public Vector3 RenderArc(Vector2 _velocity)
    {
        velocity = _velocity;
        line.positionCount = resolution + 1;
        Vector3[] points = CalculateLineArray();
        line.SetPositions(points);

        return points[resolution];
    }

    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[resolution + 1];

        var lowestTimeValue = MaxTimeX() / resolution;

        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
        }
        return lineArray;
    }

    private Vector3 HitPosition()
    {
        var lowestTimeValue = MaxTimeY() / linecastResolution;
        for (int i = 0; i < linecastResolution + 1; i++)
        {
            var t = lowestTimeValue * i;
            var tt = lowestTimeValue * (i + 1);
            RaycastHit hit; 
            Physics.Linecast(CalculateLinePoint(t), CalculateLinePoint(tt), out hit, canHit);

            if (hit.transform)
                return hit.point;
        }

        return CalculateLinePoint(MaxTimeY());
    }

    private Vector3 CalculateLinePoint(float t)
    {
        float x = velocity.x * t;
        float y = (velocity.y * t) - (g * Mathf.Pow(t, 2) / 2);
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float MaxTimeY()
    {
        var v = velocity.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * g * (transform.position.y - yLimit))) / g;
        return t;
    }

    private float MaxTimeX()
    {
        var x = velocity.x;
        if (x == 0)
        {
            velocity.x = 000.1f;
            x = velocity.x;
        }

        var t = (HitPosition().x - transform.position.x) / x;
        return t;
    }
}
