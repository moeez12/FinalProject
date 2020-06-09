using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float fireRate = 15f;
    public float nextTimeToFire;
    public float damage = 20f;
    public gunHandler handler;
   [SerializeField]private GameObject bulletrrr;
    [SerializeField]
    private Transform startBullet;
    private Camera maincamer;
    private enemy_health health;
    private void Awake()
    {
        handler =GameObject.FindGameObjectWithTag("gg").GetComponent<gunHandler>();
        maincamer = Camera.main;
        handler.firetypee =
            gunHandler.Firetype.Single;
    }



    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {   
        weaponShoot();
        ChangeFireType();
       
    }
    void weaponShoot()
    {


        if (handler.firetypee == gunHandler.Firetype.Multiple)
        {
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                handler.shootAnimation();
                handler.playshootSound();
                BulletFired();
            }

        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                handler.shootAnimation();
                BulletFired();
                handler.playshootSound();

            }
        }

    }
    void ChangeFireType()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (handler.firetypee == gunHandler.Firetype.Multiple)
            {
                handler.firetypee = gunHandler.Firetype.Single;
            }
            else handler.firetypee = gunHandler.Firetype.Multiple;
        }
    }

    void BulletFired()
    {
        GameObject clone = Instantiate(bulletrrr) as GameObject;
        GameObject bullet = Instantiate(clone);
        bullet.transform.position = startBullet.position;
        bullet.GetComponent<bulletScript>().launch(maincamer);
    }
   
}