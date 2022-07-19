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
    private Animator anim;
    private string lastIdle;
    [SerializeField] private int velocity;
    [SerializeField] private int runVelocity;
    private Vector2 move;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        lastIdle = "IdleDown";
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
            if (run.ReadValue<float>() == 1)
                rb.velocity = move * Time.fixedDeltaTime * runVelocity;
            else
                rb.velocity = move * Time.fixedDeltaTime * velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        MoveAnimate();
    }

    private void MoveAnimate()
    {
        if (move == Vector2.zero)
            anim.Play(lastIdle);
        else if (move == new Vector2(0, -1))
            AnimateSelect("Down");
        else if (move == new Vector2(-1, -1).normalized)
            AnimateSelect("DownLeft");
        else if (move == new Vector2(-1, 0))
            AnimateSelect("Left");
        else if (move == new Vector2(-1, 1))
            AnimateSelect("UpLeft");
        else if (move == new Vector2(0, 1))
            AnimateSelect("Up");
    }
    private void AnimateSelect(string play)
    {
        if (move != Vector2.zero)
        {
            lastIdle = "Idle" + play;
            if (run.ReadValue<float>() == 0)
            {
                anim.Play("Walk" + play);
            }
            else
                anim.Play("Run" + play);
        }
        else
        {
            anim.Play(lastIdle);
        }
    }
}
