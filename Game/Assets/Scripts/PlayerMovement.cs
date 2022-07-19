using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private InputAction run;
    private InputAction movement;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    [SerializeField] private int velocity;
    [SerializeField] private int runVelocity;
    private Vector2 move;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        movement = playerInputs.Gameplay.Movement;
        movement.Enable();

        run = playerInputs.Gameplay.Shift;
        run.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        playerInputs.Gameplay.Shift.Disable();
    }
    private void FixedUpdate()
    {
        Debug.Log(movement.ReadValue<Vector2>() + " " + run.ReadValue<float>());
        move = movement.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            if(run.ReadValue<float>() == 1)
                rb.velocity = move * Time.fixedDeltaTime * runVelocity;
            else
                rb.velocity = move * Time.fixedDeltaTime * velocity;

        }
        else
            rb.velocity = Vector2.zero;
    }
}
