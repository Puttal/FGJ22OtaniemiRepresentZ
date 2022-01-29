using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Racoon : MonoBehaviour
{
    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private float horizontal;
    private InputAction jumpAction;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        var gamepad = Gamepad.current;
        var keyboard = Keyboard.current;

        playerInput = GetComponent<PlayerInput>();
        InputDevice device = playerInput.GetDevice<InputDevice>();
        
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        //moveAction.performed += Move;
        jumpAction.performed += Jump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = moveAction.ReadValue<Vector2>().x;
        transform.position = new Vector2(transform.position.x + horizontal * 0.1f, transform.position.y);
    }

    // private void Move(InputAction.CallbackContext context) {
    //     horizontal = context.ReadValue<Vector2>().x;
    // }

    private void Jump(InputAction.CallbackContext context) {
        //TODO: Detect a collision below me, to allow jumping
        rigid.AddForce(transform.up * new Vector2(0, 200f), ForceMode2D.Force);
    }
}
