using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{

    private PlayerInput inputActions;

    public Vector2 direction { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Player.Move.performed += context => OnMove(context);
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    #region collback
    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        direction = context.ReadValue<Vector2>();
    }
    #endregion
}
