using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Racoon : MonoBehaviour
{
    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        var gamepad = Gamepad.current;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        jumpAction.performed += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector2 jump = jumpAction.ReadValue<Vector2>();
        float horizontal = input.x;
        //float vertical = input.y;

        transform.position = new Vector2(transform.position.x + horizontal * 0.01f, transform.position.y);
    }

    private void Jump(InputAction.CallbackContext context) {
        //TODO: Detect a collision below me, to allow jumping
        rigid.AddForce(transform.up * new Vector2(0, 200f), ForceMode2D.Force);
    }
}
