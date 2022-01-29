using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Racoon : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector2 jump = jumpAction.ReadValue<Vector2>();
        float horizontal = input.x;
        float vertical = input.y;

        transform.position = new Vector2(transform.position.x + horizontal * 0.05f, transform.position.y + vertical* 0.05f);
        
        
        
    }
}
