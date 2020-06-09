using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprint_script : MonoBehaviour
{/// <summary>
/// //////////////////This script doesnot make the player to sprint it makes it to crouch/////////////////////////
/// </summary>
    // Start is called before the first frame update


        private float crouchSpeed=2f;
    public Transform look_root;
    private float standheight = 0.9799999f;
    private float crouchHeight = 0.11f;
    private bool crouchb = false;
   
    

    private void Awake()
    {
        look_root = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    private void Start()
    {
      
        

    }
    void Update()
    {
        
        crouch();
    }
    void crouch() {
        if (Input.GetKeyUp(KeyCode.C))
        {
          
            

                look_root.localPosition = new Vector3(0f, standheight, 0f);
                crouchb = false;
            
        }
        else if  (Input.GetKeyDown(KeyCode.C))
        {

            look_root.localPosition = new Vector3(0f, crouchHeight, 0f);
            crouchb = true;





        }



    }

}
