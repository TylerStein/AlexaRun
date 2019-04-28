using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyRoom : Room
{
    public SupplyInteractive supplyInteractive;

    public override void OnFail(bool tellGameMaster = false) {
        alive = false;
    }
}
