using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Instance
    public static WaveManager Instance;
    //Waves class
    public WavesInfo wavesInfo = new WavesInfo();
    //EnemiesPooled
    private ObjectPooling objectPooling;
    public void Initialize(ObjectPooling pooling)
    {
        objectPooling = pooling;
    }
    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        WavesLogic();
        wavesInfo.ShowInfo();
        wavesInfo.WavesText();
    }
    void WavesLogic()
    {
        //Game started
        if (GameManager.Instance.gameStarted)
        {
            if (wavesInfo.AllEnemiesKilled())
            {
                wavesInfo.currentWave += 1;
                wavesInfo.enemiesSpawned = 0;
                wavesInfo.enemiesKilled = 0;
                wavesInfo.enemiesToKill += 1;
                StartWave();
            }
        }
        //Game not started
        else if (!GameManager.Instance.gameStarted)
        {
            wavesInfo.currentWave = 1;
            wavesInfo.enemiesSpawned = 0;
            wavesInfo.enemiesKilled = 0;
            wavesInfo.enemiesToKill = 10;
        }
    }
    //Start wave and adjust enemy stats
    public void StartWave()
    {
        Debug.Log("StartingNextWave");
        AdjustEnemies(wavesInfo.currentWave);
    }
    //ScaleUp enemies function
    public void AdjustEnemies(int wave)
    {
        foreach (Transform child in objectPooling.enemiesPooled) 
        {
            EnemiesInfoState state = child.gameObject.GetComponent<EnemiesInfoState>();
            state.AdjustStats(wave);
        }
    }
}
