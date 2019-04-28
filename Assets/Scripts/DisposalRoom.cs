using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalRoom : Room
{
    public List<EntityType> AllowedEntityTypes;


    public override void OnFail(bool tellGameMaster = false) {
        // alive = false;
        Debug.LogWarning("Diisposal room dieded (this shouldn't have happened)");
    }
}
