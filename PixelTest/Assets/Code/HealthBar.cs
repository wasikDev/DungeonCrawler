using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    
    
   // public Transform target;
    private void Update()
    {
        //transform.eulerAngles = new Vector3(x, fixedRotation, z);
        gameObject.transform.localScale = new Vector3(1, 1, 1);

    }

    public void setMaxHealth (int maxHealt)
    {
        slider.maxValue = maxHealt; 
        slider.value = maxHealt;
    }
    
    public void setHealth(int health)
    {
        slider.value = health;
        Debug.Log("Health bar set to: " + health);
    }

    // Method to call when the player takes damage
    

}
