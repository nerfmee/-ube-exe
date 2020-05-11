using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Enemies/Standart Enemy", fileName ="New Enemy")]
public class EnemyData : ScriptableObject
{
    [Tooltip("Основной спрайт")]
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { }
    }

    [Tooltip("Скорость врага")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        protected set { }
    }

    [Tooltip("Атака врага")]
    [SerializeField] private float attack;
    public float Attack
    {
        get { return attack; }
        protected set { }
    }


}
