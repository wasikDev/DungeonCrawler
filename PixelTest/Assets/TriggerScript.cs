using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
   
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag=="Player")
        if (obj1 != null)
                obj1.SetActive(true);
        if (obj2 != null)
            obj2.SetActive(true);
        if (obj3 != null)
            obj3.SetActive(true);
    }
       
           
       

       


    
}
