using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    Vector2 move;
    bool changeLookFlag;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public Vector2 GetMove()
    {
        return move;
    }

    public bool GetChangeLookFlag()
    {
        return changeLookFlag;
    }

    public bool SetChangeLookFlag(bool flag)
    {
        return changeLookFlag = flag;
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void InputChangeLook(InputAction.CallbackContext context)
    {
        if (context.performed && !changeLookFlag) changeLookFlag = true;
    }
}
