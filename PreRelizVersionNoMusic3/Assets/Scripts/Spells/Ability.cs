using UnityEngine;
using System.Collections;



public abstract class Ability : ScriptableObject
{
    public string aName = "New Ability";
    public Sprite aSprite;
    public AudioClip aSound;
    public float aBaseCoolDown = 1f;
    public float aDuration = 0f;    //работает ли способность
    public int aID = -1; 

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
