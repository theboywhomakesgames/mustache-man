using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InputManager", fileName = "InputManager")]
public class Inputs : ScriptableObject
{
    public SimpleInput walkRight;
    public SimpleInput walkLeft;
    public SimpleInput jump;
    public SimpleInput shoot;
    public SimpleInput reload;
    public SimpleInput slowMo;
}
