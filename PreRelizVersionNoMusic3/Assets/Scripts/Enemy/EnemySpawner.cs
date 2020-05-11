using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Список настроек для врагов")]
    [SerializeField] private List<EnemyData> enemySettings;

    [Tooltip("Количество объектов в пуле")]
    [SerializeField] public float poolCount;

    [Tooltip("Ссылка на базовый префаб для врагов")]
    [SerializeField] private GameObject enemyPrefab;

    [Tooltip("Время между спауном врагов")]
    [SerializeField] public float spawnTime = 0.5f;

    [SerializeField] private Text waveNumberText;
    private int waveNumber = 1;

    

    [SerializeField] private Player player;
    
    
    
    private float countsOfenemy = 10f;

    private bool spawn = true;
    
    public static Dictionary<GameObject, Enemy> Enemies;

    private Queue<GameObject> currentEnemies;

    
    
    private void Start()
    {
        Enemies = new Dictionary<GameObject, Enemy>();
        currentEnemies = new Queue<GameObject>();

        for (int i = 0; i < poolCount; ++i)
        {
            var prefab = Instantiate(enemyPrefab);
            var script = prefab.GetComponent<Enemy>();
            prefab.SetActive(false);
            Enemies.Add(prefab, script);
            currentEnemies.Enqueue(prefab);
        }
        Enemy.OnEnemyOverFly += ReturnEnemy;
        
        StartCoroutine(Spawn());
        
    }

    private IEnumerator Spawn()
    {
        if(this != null)
        {
 
            spawn = true;
            if (spawnTime == 0)
            {
                Debug.LogError("Не выставлено время спауна, задано стандартное значение - 1 сек.");
                spawnTime = 1;
            }
            while (spawn)
            {
                yield return new WaitForSeconds(spawnTime);
                if (currentEnemies.Count>0)
                {
                    SpawnWaves();
                }

            }
 
        }
        
    }

    //Возврат врага обратно в пул и подготовка к повторному использованию
    private void ReturnEnemy(GameObject _enemy)
    {
        if(this!=null)
        {
 
            _enemy.transform.position = transform.position;
            _enemy.SetActive(false);
            currentEnemies.Enqueue(_enemy);
 
        }
    }
    void SpawnWaves()
    {
        if(this!=null)
        {
 
            // получение компонентов и активация врага
            var enemy = currentEnemies.Dequeue();
            var script = Enemies[enemy];
            enemy.SetActive(true);
            // генерация случайного EnemyData и инициализация
            int rand = Random.Range(0, enemySettings.Count);
            script.Init(enemySettings[rand]);

            float xPos = Random.Range(-GameCamera.Border + 0.4f, GameCamera.Border - 0.35f);
            enemy.transform.position = new Vector2(xPos, transform.position.y);
 
        }
      
    }


   private void Update()
    {
        waveNumberText.text = "Wave: " + waveNumber.ToString();
           
        
        
        if (poolCount <= 0)
        {
            spawn = false; 
          SpawnNewWave();
        }
        
    }


   void SpawnNewWave()
   {
       if(this!=null)
       {
 
           waveNumber++;
       
           spawn = true;
           countsOfenemy += 3; 
           poolCount += countsOfenemy;

           spawnTime -= 0.025f;
       
           for (int i = 0; i < poolCount; ++i)
           {
               var prefab = Instantiate(enemyPrefab);
               var script = prefab.GetComponent<Enemy>();
               prefab.SetActive(false);
               Enemies.Add(prefab, script);
               currentEnemies.Enqueue(prefab);
           }
           Enemy.OnEnemyOverFly += ReturnEnemy;
           StartCoroutine(Spawn());
 
       }
       
   }
   

   
}
