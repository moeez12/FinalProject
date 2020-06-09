using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float HealthPoints
    {

        get { return healthPoints; }


        set {
            healthPoints = value;
            if (healthPoints <= 0)  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        

}
    private void Update()
    {
        if (healthPoints <= 0)
        {
            Application.Quit();
           
        }
    }
    [SerializeField] private float healthPoints = 200f;
}
