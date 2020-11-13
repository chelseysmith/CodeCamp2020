using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float _playerSpeed;
    [SerializeField] float _rotationSpeed;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var leftStick = Gamepad.current.leftStick.ReadValue();
        var rightStick = Gamepad.current.rightStick.ReadValue();

        if(Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("A");
        }

        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            Debug.Log($"Left trigger: {Gamepad.current.leftTrigger.ReadValue()}");
        }

        var direction = new Vector3(leftStick.x, 0, leftStick.y).normalized;
        if(direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            _characterController.Move(direction * Time.deltaTime * _playerSpeed);
        }
    }
}
