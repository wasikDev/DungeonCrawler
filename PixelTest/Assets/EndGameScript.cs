using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    private HealthManager healthManager;
    public GameObject button1;
    public GameObject button2;
    public bool menuIsActive = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        healthManager = GameObject.Find("GoblinKing").GetComponent<HealthManager>();

    }


    private void Update()
    {
        if (healthManager == null)
        {
            return;
        }
        if ((healthManager.currentHealth < 0))
        {
            Time.timeScale = 0;

          
                    GameObject.Find("GoblinKing").SetActive(false);

            button1.SetActive(true);
            button2.SetActive(true);
            menuIsActive = true;


        }

       

    }

   

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}
