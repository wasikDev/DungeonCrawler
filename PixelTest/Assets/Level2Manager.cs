using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    private HealthManager healthManager;
    public GameObject button1;
    public GameObject button2;
    public bool menuIsActive = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        healthManager = GameObject.Find("Player").GetComponent<HealthManager>();

    }

    
    private void Update()
    {
        if(healthManager == null)
        {
            return;
        }
        if((healthManager.currentHealth < 0 || Input.GetKeyDown(KeyCode.Z)) && menuIsActive==false)
        {
            Time.timeScale = 0;

            if(healthManager.currentHealth < 0) 
                if(GameObject.Find("Player") != null)
                    GameObject.Find("Player").SetActive(false);

            button1.SetActive(true);
            button2.SetActive(true);
            menuIsActive = true;


        }

        if (Input.GetKeyDown(KeyCode.Q) && menuIsActive == true)
        {
            Time.timeScale = 1;

          

            button1.SetActive(false);
            button2.SetActive(false);
            menuIsActive = false;


        }

    }
    
    public void Restart()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);

        if (SceneManager.GetActiveScene().name == "Level1")
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);

        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
