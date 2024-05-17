using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int selectedCharacterIndex; // 0 dla postaci 1, 1 dla postaci 2

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Zapobiega zniszczeniu obiektu przy zmianie sceny
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

