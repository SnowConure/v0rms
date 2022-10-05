using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public class BasicInputSystem : MonoBehaviour
    {
        public Action<float> MovementDirection;

        public Action Jump;

        public Action Camera;


        public Action FireDown, FireUp;

        public Action AimDown, AimUp;

        public Action Moving;
    }


}
