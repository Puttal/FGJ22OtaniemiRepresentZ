using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Racoon : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private float horizontal;
    private InputAction jumpAction;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

        if (horizontal > 0) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (horizontal != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    // private void Move(InputAction.CallbackContext context) {
    //     horizontal = context.ReadValue<Vector2>().x;
    // }

    private void Jump(InputAction.CallbackContext context) {
        //TODO: Detect a collision below me, to allow jumping
        rigid.AddForce(transform.up * new Vector2(0, 200f), ForceMode2D.Force);
    }
}
