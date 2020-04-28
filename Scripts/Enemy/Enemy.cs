using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData data;
    
    //Инициализация врага 
    public void Init(EnemyData _data)
    {
        data = _data;
        GetComponent<SpriteRenderer>().sprite = data.MainSprite;
    }

    //Атака текущего врага 
    public float Attack
    {
        get { return data.Attack; }
        protected set { }
    }

    public static Action<GameObject> OnEnemyOverFly;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * data.Speed);

        // В случае когда мы вылетели за пределы экрана 
        // С помощью события запрашиваем возврат нас обратно в пул
        if (transform.position.y > 6 && OnEnemyOverFly != null )
        {
            OnEnemyOverFly(gameObject);
        }

        if (transform.position.y > 7)
        {
           Destroy(gameObject);
        }


    }

}
