using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public GameObject wizzard; // Przypisz postacie w edytorze

    public GameObject warrior;

    void Start()
    {
        if (GameManager.Instance.selectedCharacterIndex == 0)
        {
            warrior.SetActive(true);
           
        }

        if (GameManager.Instance.selectedCharacterIndex == 1)
        {
            wizzard.SetActive(true);
            
        }
    }
}


