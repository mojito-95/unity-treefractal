using UnityEngine;
using UnityEngine.UI;

public class TreeFractalUI : MonoBehaviour
{
    [SerializeField]
    private Slider _recursive, _len, _lenMultiplier, _angle;
    [SerializeField]
    private Button _generateFractal;
    [SerializeField]
    private Toggle _threeDimension, _autoAngle;
    [SerializeField]
    private TreeFractal _treeFractal;

    [SerializeField]
    [Range(0.0001f, 0.003f)]
    private float _autoAngleDelay = 0.0003f;
    private float _nextAngle;

    private void Awake()
    {
        _recursive.onValueChanged.AddListener(ChangeRecursive);
        _len.onValueChanged.AddListener(ChangeLen);
        _lenMultiplier.onValueChanged.AddListener(ChangeLenMultiplier);
        _angle.onValueChanged.AddListener(ChangeAngle);
        _threeDimension.onValueChanged.AddListener(ChangeThreeDimension);
        _autoAngle.onValueChanged.AddListener(ChangeAutoAngle);
        _generateFractal.onClick.AddListener(GenerateFractal);

        _treeFractal.recursive = (int)_recursive.value;
        _treeFractal.len = _len.value;
        _treeFractal.lenMultiplier = _lenMultiplier.value;
        _angle.value = 0f;
        _treeFractal.threeDimension = _threeDimension.isOn;
    }

    private void Update()
    {
        if (_autoAngle.isOn)
        {
            if (Time.time > _nextAngle)
            {
                _nextAngle = Time.time + _autoAngleDelay;
                _treeFractal.leftAngle += 0.01f;
                _treeFractal.rightAngle = 0.02f;

                _treeFractal.GenerateFractal(false);
            }
        }
    }

    private void ChangeRecursive(float recursive)
    {
        _treeFractal.recursive = (int)recursive;

        if (!_autoAngle.isOn)
        {
            _treeFractal.GenerateFractal(true);
        }
    }

    private void ChangeLen(float len)
    {
        _treeFractal.len = len;

        _treeFractal.GenerateFractal(false);
    }

    private void ChangeLenMultiplier(float lenMultiplier)
    {
        _treeFractal.lenMultiplier = lenMultiplier;

        _treeFractal.GenerateFractal(false);
    }

    private void ChangeAngle(float angle)
    {
        _treeFractal.leftAngle = (Mathf.PI / 4) + angle;
        _treeFractal.rightAngle = (Mathf.PI / 4) + angle;

        _treeFractal.GenerateFractal(false);
    }

    private void ChangeThreeDimension(bool threeDimension)
    {
        _treeFractal.threeDimension = threeDimension;

        if (!_autoAngle.isOn)
        {
            _treeFractal.GenerateFractal(true);
        }
    }

    private void ChangeAutoAngle(bool autoAngle)
    {
        if (!autoAngle)
        {
            _treeFractal.leftAngle = (Mathf.PI / 4) + _angle.value;
            _treeFractal.rightAngle = (Mathf.PI / 4) + _angle.value;

            _treeFractal.GenerateFractal(false);
        }
    }

    private void GenerateFractal()
    {
        _treeFractal.GenerateFractal(true);
    }
}
