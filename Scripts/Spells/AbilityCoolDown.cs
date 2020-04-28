using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour
{
   
    public Image darkMask;
    public Text coolDownTextDisplay;

    [SerializeField] private Ability ability;
   
    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    public float nextDurationTime;
    private float coolDownTimeLeft;
    
    public int id;

    public Button button;
    private float abilityDuration;
    public float durationTimeLeft;
    public bool immune;



    bool click;

    
    

    void Start()
    {
        Initialize(ability);
    }
   

 

    public void Initialize(Ability selectedAbility)
    {
       
        Button btn= button.GetComponent<Button>();
        btn.onClick.AddListener(TestClick);
        
        
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.aSprite;
        darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.aBaseCoolDown;
        id = ability.aID;

        abilityDuration = ability.aDuration;

        
       

        AbilityReady();
    }
    
    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
     
        if (coolDownComplete)
        {
            AbilityReady();
            if (click==true)
            {
                ButtonTriggered();
            }
           
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        // Debug.Log(abilityDuration);
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        click = false;
        coolDownTimeLeft -= Time.deltaTime;

        durationTimeLeft -= Time.deltaTime;

        if (durationTimeLeft > 0)
        {
            immune = true;
        }
        else
        {
            immune = false;
        }
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    public void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        nextDurationTime = abilityDuration + Time.time;

        coolDownTimeLeft = coolDownDuration;
        durationTimeLeft = abilityDuration;


        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
        



        //abilitySource.clip = ability.aSound;
        //abilitySource.Play();
        ability.TriggerAbility();
    }

    void TestClick()
    {
      
        click = true;
       
        //Debug.Log("клик клик клик");
    }



}
