using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDuration : MonoBehaviour
{
    [SerializeField] private AbilityCoolDown abilityCoolDownS;

    public Image darkMask;
    public float duration;
    public Text durationTextDisplay;
   
    [SerializeField] private Image sprite;

    public void Initialize(AbilityCoolDown abilityCoolDown)
    {
        //sprite = GetComponent<SpriteRenderer>();
        abilityCoolDownS = abilityCoolDown;
        duration = abilityCoolDownS.durationTimeLeft;
    }

    private void Start()
    {
        Initialize(abilityCoolDownS);
    }

    private void Update()
    {
        float roundedDur = Mathf.Round(abilityCoolDownS.durationTimeLeft);
        if (roundedDur >= 0)
        {
            durationTextDisplay.text = roundedDur.ToString();
        }
        if (roundedDur <= 0)
        {
            durationTextDisplay.enabled = false;
            sprite.enabled = false;
        }

        if (abilityCoolDownS.durationTimeLeft != 0.0f && abilityCoolDownS.durationTimeLeft>0)
        {
            sprite.enabled = true;
            durationTextDisplay.enabled = true;
            darkMask.enabled = true;
            darkMask.fillAmount = (abilityCoolDownS.durationTimeLeft / abilityCoolDownS.nextDurationTime);
         
        }
        else
        {
            darkMask.enabled = false;
        }
      
    }
}