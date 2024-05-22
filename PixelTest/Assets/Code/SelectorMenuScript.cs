using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorMenuScript : MonoBehaviour
{
   

    public void SelectWarrior()
    {
        GameManager.Instance.selectedCharacterIndex = 0;
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void SelectWizzard()
    {
        GameManager.Instance.selectedCharacterIndex = 1;
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
