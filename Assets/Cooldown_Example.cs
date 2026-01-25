using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float redColourDuration = 1;
    public float currentTimeInGame;
    public float lastTimeWasDamaged;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeColourIfNeeded();
    }

    private void ChangeColourIfNeeded()
    {
        // this acts as cooldown by also using the redColourDuration as the default value
        currentTimeInGame = Time.time;
        if (currentTimeInGame > lastTimeWasDamaged + redColourDuration)
        {
            if (sr.color!= Color.white)
                sr.color = Color.white;
        }
    }


    public void TakeDamage()
    {
        sr.color = Color.red;
        lastTimeWasDamaged = Time.time;
    }

    private void TurnWhite()
    {
        sr.color = Color.white;
    }
}
