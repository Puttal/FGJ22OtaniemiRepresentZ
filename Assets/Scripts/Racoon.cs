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
    private InputAction interAction;
    

    private bool canJump = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        var gamepad = Gamepad.current;
        var keyboard = Keyboard.current;

        playerInput = GetComponent<PlayerInput>();
        InputDevice device = playerInput.GetDevice<InputDevice>();
        
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        interAction = playerInput.actions["Interact"];

        //moveAction.performed += Move;
        jumpAction.performed += Jump;
        interAction.performed += Interact;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = moveAction.ReadValue<Vector2>().x;
        transform.position = new Vector2(transform.position.x + horizontal * 0.1f, transform.position.y);

        if (horizontal > 0) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (horizontal < 0) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Jump(InputAction.CallbackContext context) {
        if (canJump) {
        //TODO: Detect a collision below me, to allow jumping
            rigid.AddForce(transform.up * new Vector2(0, 500f), ForceMode2D.Force);
        }
    }

    private void Interact(InputAction.CallbackContext context) {
        //TODO
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts.Length > 0)
        {
            for (int i = 0; i < collision.contacts.Length; i++) {
                ContactPoint2D contact = collision.GetContact(i);
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    Debug.Log("Can Jump");
                    canJump = true;
                }
            }
        } else {
            canJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.contacts.Length > 0)
        {
            for (int i = 0; i < collision.contacts.Length; i++) {
                ContactPoint2D contact = collision.GetContact(i);
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    Debug.Log("Can Jump");
                    canJump = true;
                }
            }
        } else {
            canJump = false;
        }
    }
}
