using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Status")]
public class CharacterStatus : ScriptableObject
{
    public bool isAiming;
    public bool isRunning;
    public bool isGround;
}
