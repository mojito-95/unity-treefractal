using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TreeFractal : MonoBehaviour
{
    private int _recursive;
    private float _len, _lenMultiplier;
    private float _leftAngle, _rightAngle;
    private bool _threeDimension;

    private int _delay;

    private List<TreeLine> _lines = new List<TreeLine>();

    private void Awake()
    {
        _leftAngle = Mathf.PI / 4;
        _rightAngle = Mathf.PI / 4;

        _delay = 40;
    }

    public int recursive
    {
        set { if (value > 0) { _recursive = value; } }
        get { return _recursive; }
    }

    public float len
    {
        set { if (value > 0) { _len = value; } }
        get { return _len; }
    }

    public float lenMultiplier
    {
        set { if (value > 0) { _lenMultiplier = value; } }
        get { return _lenMultiplier; }
    }

    public float leftAngle
    {
        set { if (value >= Mathf.PI / 4) { _leftAngle = value; } }
        get { return _leftAngle; }
    }

    public float rightAngle
    {
        set { if (value >= Mathf.PI / 4) { _rightAngle = value; } }
        get { return _rightAngle; }
    }

    public bool threeDimension
    {
        set { _threeDimension = value; }
        get { return _threeDimension; }
    }

    public void GenerateFractal(bool delay)
    {
        _lines.Clear();

        CreateTree(new Vector3(0f, 0f, 0f), _len, Mathf.PI / 2, _recursive, _threeDimension, delay);
    }

    private async void CreateTree(Vector3 position, float len, float angle, int recursive, bool threeDimension, bool delay)
    {
        if (recursive == 0) { return; }
        Vector3 newPosition = Vector3.zero;
        if (threeDimension)
        {
            newPosition = position + new Vector3(Mathf.Cos(angle) * Mathf.Sin(angle), Mathf.Sin(angle) * Mathf.Sin(angle), Mathf.Cos(angle)) * len;
        }
        else
        {
            newPosition = position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * len;
        }
        _lines.Add(new TreeLine(position, newPosition));
        if (delay)
        {
            await Task.Delay(_delay);
        }

        CreateTree(newPosition, len * _lenMultiplier, angle + _leftAngle, recursive - 1, threeDimension, delay);
        CreateTree(newPosition, len * _lenMultiplier, angle - _rightAngle, recursive - 1, threeDimension, delay);
    }

    private void OnDrawGizmos()
    {
        if (_lines.Count == 0) { return; }

        Gizmos.color = Color.white;
        foreach (TreeLine line in _lines)
        {
            Gizmos.DrawLine(line.StartPosition, line.EndPosition);
        }
    }
}

public struct TreeLine
{
    public Vector3 StartPosition { private set; get; }
    public Vector3 EndPosition { private set; get; }

    public TreeLine(Vector3 startPosition, Vector3 endPosition)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
    }
}
