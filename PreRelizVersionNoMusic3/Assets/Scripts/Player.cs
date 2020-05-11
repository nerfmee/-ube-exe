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




    private Vector2 startpositionMove;
    private float targetPos;

    private float dirX;

    [SerializeField] private float _speedLerp;



   [SerializeField] private Vector2 _direction;
    private float _smoothing = 0.25f;

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
        if ((p_abilitys[0].immune == true || p_abilitys[1].immune == true))
        {
            sprite.enabled = false; // выключаем белый квадрат, и появляется эффект иммуна
        }
        else if ((p_abilitys[0].immune != true || p_abilitys[1].immune != true))
        {
            sprite.enabled = true;
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
        if (Input.GetKey(KeyCode.Q))
        {
            deflect.onClick.Invoke();
        }
        if (Input.GetKey(KeyCode.W))
        {
            force.onClick.Invoke();
        }
          Movement();
    }


    public void Movement()
    {

        if (Input.GetKey(KeyCode.A) && transform.position.x > (-GameCamera.Border + 0.4f))
        {
            _direction.x = 0f;
            _direction.x = 1f;
            ismovement = true;
           transform.Translate(Vector3.left * speed);
            rigidbody2D.AddForce(-_direction.normalized * _speedLerp, ForceMode2D.Impulse);

        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < (GameCamera.Border - 0.4f))
        {
            _direction.x = 0f;
            _direction.x = 1f;
            ismovement = true;
               transform.Translate(Vector3.right * speed);
            rigidbody2D.AddForce(_direction.normalized * _speedLerp, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;

        if (obj.tag == "RedSquare" && (p_abilitys[0].immune == false && p_abilitys[1].immune == false))
        {
                isAlivePlayer = false;
                gameObject.SetActive(false);
               
        }
        else if (obj.tag == "RedSquare" && (p_abilitys[0].immune == true   || p_abilitys[1].immune == true) )
        {
            coinsInt += 200;
            
            CameraShake.Shake(0.2f,0.5f);
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            
            Destroy(obj);
            
            enemySpawn.poolCount -= 0.5f;
        }


    }
 }

