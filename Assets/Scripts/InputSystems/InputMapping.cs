using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "InputSystem", order = 1)]
public class InputMapping : ScriptableObject
{
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Jump = KeyCode.W;
    public KeyCode Camera = KeyCode.Space;
    public int Aim = 1;
    public int Fire = 0;

}
