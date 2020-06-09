using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    public float HealthPoints
    {

        get { return healthPoints; }


        set
        {
            healthPoints = value;
            if (healthPoints <= 0) Destroy(gameObject);
        }



    }
    [SerializeField] private float healthPoints = 20000f;

   
}

