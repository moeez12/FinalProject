﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour
{
    // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }
    void Change() {

        if (Input.GetKeyDown(KeyCode.Escape)) {


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}