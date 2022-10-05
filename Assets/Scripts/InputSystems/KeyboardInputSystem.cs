using InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputSystem : BasicInputSystem
{
    [SerializeField]
    private InputMapping inputMapping;

    private float movementDirection;

    void Update()
    {

        if (Input.GetKeyDown(inputMapping.Left)) movementDirection += -1;
        if (Input.GetKeyUp(inputMapping.Left)) movementDirection -= -1;

        if (Input.GetKeyDown(inputMapping.Right)) movementDirection += 1;
        if (Input.GetKeyUp(inputMapping.Right)) movementDirection -= 1;

        MovementDirection?.Invoke(movementDirection);

        if (Input.GetKeyDown(inputMapping.Jump)) Jump?.Invoke();

        if (Input.GetMouseButtonDown(inputMapping.Fire)) FireDown?.Invoke();
        if (Input.GetMouseButtonUp(inputMapping.Fire)) FireUp?.Invoke();

        if (Input.GetMouseButtonDown(inputMapping.Aim)) AimDown?.Invoke();
        if (Input.GetMouseButtonUp(inputMapping.Aim)) AimUp?.Invoke();

        if (Input.GetKeyDown(inputMapping.Camera)) Camera?.Invoke();

        if (Input.GetKey(inputMapping.Left) || Input.GetKey(inputMapping.Right)) Moving?.Invoke();
    }
}
