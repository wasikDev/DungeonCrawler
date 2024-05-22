using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarlockBossScript : MonoBehaviour
{
    public HealthManager healthManager;
    public GameObject bat1;
    public GameObject bat2;
    public GameObject bat3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthManager.currentHealth < 50)
        {
            if(bat1 != null)
            bat1.SetActive(true);
            if (bat2 != null)
                bat2.SetActive(true);
            if (bat3 != null)
                bat3.SetActive(true);
        }

       

        
    }
}
