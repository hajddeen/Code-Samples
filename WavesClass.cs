using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class WavesInfo
{
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI wavesText;
    public int currentWave = 1;
    public int enemiesSpawned;
    public int enemiesToKill = 10;
    public int enemiesKilled;
    //Check if can spawn more enemies
    public bool CanSpawnEnemies()
    {
        if (enemiesSpawned < enemiesToKill) return true;
        else return false;
    }
    //Check if can proceed to next wave
    public bool AllEnemiesKilled()
    {
        if (enemiesKilled == enemiesToKill)
        {
            return true;
        }
        else if (enemiesKilled < enemiesToKill)
        {
            return false;
        }
        else return false;
    }
    //Debugging purposes
    public void DebugInfo()
    {
        Debug.Log("Current wave: " +  currentWave + "Enemies killed: " + enemiesKilled + "Spawned enemies: " + enemiesSpawned + "Total enemies to kill: " + enemiesToKill);
        Debug.Log("Enemies killed in wave? " + AllEnemiesKilled());
    }
    //Update text
    public void ShowInfo()
    {
        enemiesText.text = enemiesKilled.ToString() + "/" + enemiesToKill.ToString();
    }
    //Update waves
    public void WavesText()
    {
        wavesText.text = "Wave #" + currentWave.ToString();
    }
}
