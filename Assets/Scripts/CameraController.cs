using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 16f)]
    private float _movementSpeed = 4f;
    [SerializeField]
    [Range(200f, 600f)]
    private float _rotationSpeed = 400f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Movement();
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Orbit();
            }
        }
    }

    private void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float verticalInput = Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Horizontal") * _movementSpeed * Time.deltaTime;

            transform.Translate(new Vector3(horizontalInput, 0f, verticalInput), Space.Self);
        }
    }


    private void Orbit()
    {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.right, -verticalInput, Space.Self);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }
}