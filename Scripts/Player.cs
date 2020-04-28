using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    [Tooltip("Начальная позиция игрока")]
    [SerializeField] private Vector3 startPos;
    
    
    [Tooltip("Начальное количество здоровья")]
    [SerializeField] private float health = 5.0f;

    [Tooltip("Скорость игрока")] [SerializeField]
    public float speed;

    [SerializeField] private List <AbilityCoolDown>  p_abilitys;
    [SerializeField] private  ForceAbility  forceAbility;
    [SerializeField] private  DeflectAbility  deflectAbility;

    public Rigidbody2D rigidbody2D;
    [SerializeField]private float jumpForce = 10f;
    SpriteRenderer sprite;
    
    public GameObject impactEffect;
    public GameObject redImpactEffect;
    public bool ismovement = false;

    public int coinsInt ;
    [SerializeField ]private Text coinsText;
    [SerializeField] private Text ememyText;
    [SerializeField ] public  GameObject  healthh;
    [SerializeField] private EnemySpawner enemySpawn;
    
    
    public Button deflect;
    public Button force;

    private int spellID;

    public bool isAlivePlayer = true;

    public CameraShake cameraShake;


    public ClickScript [] ControlSQR;

    private Vector2 startpositionMove;
    private float targetPos;

    private float dirX;

    [SerializeField] private GameController gameController;
    
    
    private void Start()
    {
        
        
        sprite = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Initialize(p_abilitys[spellID]);
        transform.position = startPos;
        

    }
    
    public void Initialize(AbilityCoolDown selectedAbility)
    {
        spellID = selectedAbility.id;
        p_abilitys[spellID] = selectedAbility;
        
    }


    private void Update()
    {

 

        if (ControlSQR[0].clickedIs)
        {
            deflect.onClick.Invoke();
        }
        if (ControlSQR[1].clickedIs)
        {
            force.onClick.Invoke();
        }
        

        coinsText.text = coinsInt.ToString();
        
        ememyText.text = "Enemy: " + enemySpawn.poolCount.ToString();
        Jump();
    }

   public void Jump()
    {
        if ( forceAbility.force == true)
        {
            ismovement = true;
            rigidbody2D.AddForce(Vector2.down * jumpForce);
        }
        forceAbility.force = false;
    }




    private void FixedUpdate()
    {
        //rigidbody2D.velocity = new Vector2(dirX * speed, rigidbody2D.velocity.y);
        if (Input.GetKey(KeyCode.Q))
        {
            deflect.onClick.Invoke();
        }
        if (Input.GetKey(KeyCode.W))
        {
            force.onClick.Invoke();
        }

        if ((p_abilitys[0].immune == true   || p_abilitys[1].immune == true))
        {
            sprite.enabled = false; // выключаем белый квадрат, и появляется эффект иммуна
        }
        else if ((p_abilitys[0].immune != true   || p_abilitys[1].immune != true))
        {
            sprite.enabled = true;
        }
          Movement();
    }


    public void Movement()
    {

        if (Input.GetKey(KeyCode.A) && transform.position.x > (-GameCamera.Border + 0.4f))
        {
            ismovement = true;
            transform.Translate(Vector3.left * speed);
            
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < (GameCamera.Border - 0.4f))
        {
            ismovement = true;
            transform.Translate(Vector3.right * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;

        if (obj.tag == "RedSquare" && (p_abilitys[0].immune == false && p_abilitys[1].immune == false) && health<=0)
        {
            isAlivePlayer = false;
//            Debug.Log(p_abilitys[spellID]);
            gameObject.SetActive(false);
                //Destroy(gameObject, 0.3f);
            
        }
        else if (obj.tag == "RedSquare" && (p_abilitys[0].immune == true   || p_abilitys[1].immune == true) )
        {
            coinsInt += 200;
           
        //    Debug.Log(p_abilitys[spellID].immune);
            
            CameraShake.Shake(0.4f,0.5f);
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            
            Destroy(obj);
            
            enemySpawn.poolCount -= 0.5f;
        }

        if (EnemySpawner.Enemies.ContainsKey(obj) && (p_abilitys[0].immune == false && p_abilitys[1].immune == false))
        {
            int i = 0;
            health -= EnemySpawner.Enemies[obj].Attack;
            healthh.SetActive(false);
            
            GameObject effectIns = Instantiate(redImpactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Destroy(obj);


            enemySpawn.poolCount -= 1f;


//            Debug.Log("Игрок получил серьёзные повреждения" + health + "hp");





        }

    }


  
}
