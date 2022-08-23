using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTeleportStateData", menuName = "Data/State Data/Teleport State")]
public class D_TeleportState : ScriptableObject
{
    public Vector3 targetLocation = Vector3.zero;
}
