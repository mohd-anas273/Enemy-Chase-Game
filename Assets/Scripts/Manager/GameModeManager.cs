using System;
using UnityEngine;
public class GameModeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameModePrefabs;

    public Type SelectGameMode(int gameMode)
    {
        if (gameMode >= gameModePrefabs.Length)
        {
            Debug.LogWarning("GameMode Index reach out of bound");
            return null;
        }
        Type t = gameModePrefabs[gameMode].GetComponent<BaseInputMode>().GetType();
        return t;
    }
}