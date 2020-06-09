using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private Rigidbody mybody;
    public float speed = 50f;
    public float deactivate_timer = 3f;
    public float damage = 15f;
   public enemy_health health1,health2;
    private GameObject parent;
    List<GameObject> currentCollisions = new List<GameObject>();


    // Start is called before the first frame update

    private void Awake()
    {
        mybody = GetComponent<Rigidbody>();
       
        
       // health2 = GameObject.Find("enemy2").GetComponent<enemy_health>();

    }
    void Start()
    {
       // Invoke("deactivateGameObject", deactivate_timer);
    }

    public void launch(Camera mainCamera) {
        mybody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + mybody.velocity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void deactivateGameObject() {
        if (gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
        }
    }
        private void OnTriggerEnter(Collider collision)
        { currentCollisions.Add (collision.gameObject);

        // Print the entire list to the console.
        if (!(currentCollisions == null)) 
        {
            foreach (GameObject gob in currentCollisions)
            {
                if (!(gob == null))
                {
                    if (gob.tag == "ssss")
                    {
                        parent = gob.transform.root.gameObject;
                        health1 = parent.GetComponent<enemy_health>();
                        health1.HealthPoints -= 10;

                        currentCollisions.Clear();
                        break;
                    }
                    else if (gob.tag == "esss")
                    {
                        parent = gob.transform.root.gameObject;
                        health2 = parent.GetComponent<enemy_health>();
                        health2.HealthPoints -= 10;

                        currentCollisions.Clear();
                        break;

                    }

                }

            }
        }

        }
    }

