using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[CreateAssetMenu(menuName = "Abilities/ForceAbility")]

public class ForceAbility : Ability
{
    private Rigidbody2D rigidbody2D;
    private Player pRayer;
   
    public bool force;


    
    public override void Initialize(GameObject obj)
    {

    }

    public override void TriggerAbility()
    {
        force = true;
    }


}
