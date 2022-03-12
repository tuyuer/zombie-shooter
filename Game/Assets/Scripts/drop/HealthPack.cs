using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : DropElement
{
    public int remedyValue = 10;
    public override void UseElement()
    {
        GameWorld.Instance.player.Blood.OnRemedy(remedyValue);
    }
}
