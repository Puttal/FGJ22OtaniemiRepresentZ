using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Racoon : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigid;
    private BoxCollider2D colider;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private float horizontal;
    private InputAction jumpAction;
    private InputAction interAction;
    private InputAction chooseA;
    private InputAction chooseB;
    

    private bool canJump = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        colider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        var gamepad = Gamepad.current;
        var keyboard = Keyboard.current;

        playerInput = GetComponent<PlayerInput>();
        InputDevice device = playerInput.GetDevice<InputDevice>();
        
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        interAction = playerInput.actions["Interact"];

        chooseA = playerInput.actions["ChooseA"];
        chooseB = playerInput.actions["ChooseB"];

        //moveAction.performed += Move;
        jumpAction.performed += Jump;
        interAction.performed += Interact;
        chooseA.performed += ChooseA;
        chooseB.performed += ChooseB;

        GameMaster.Instance.AddRacoon(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = moveAction.ReadValue<Vector2>().x;
        transform.position = new Vector2(transform.position.x + horizontal * 0.1f, transform.position.y);

        if (horizontal > 0) {
            transform.localScale = new Vector3(-.5f, .5f, 1f);
        } else if (horizontal < 0) {
            transform.localScale = new Vector3(.5f, .5f, 1f);
        }

        if (horizontal != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    private void Jump(InputAction.CallbackContext context) {
        if (CanJump()) {
        //TODO: Detect a collision below me, to allow jumping
            rigid.AddForce(transform.up * new Vector2(0, 500f), ForceMode2D.Force);
            canJump = false;
        }
    }

    private void Interact(InputAction.CallbackContext context) {
        GameMaster.Instance.GetCurrentEvent().RacoonConfirm(this);
    }

    private void ChooseA(InputAction.CallbackContext context) {
        GameMaster.Instance.GetCurrentEvent().RacoonChoose(this, Event.EventOption.A);
    }
    
    private void ChooseB(InputAction.CallbackContext context) {
        GameMaster.Instance.GetCurrentEvent().RacoonChoose(this, Event.EventOption.B);
    }

    private bool CanJump() {
        return canJump;
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
}
