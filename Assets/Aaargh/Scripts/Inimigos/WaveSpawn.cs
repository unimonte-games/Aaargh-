using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    public Transform[] spawnPoints;

    private int nextWave = 0;
    private float seachCountdown = 1f;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public SpawnState state = SpawnState.COUTING;

    void Start()
    {
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("Não há pontos de spawn referentes");
        }
        waveCountdown = timeBetweenWaves;  
    }
    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime; 
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.COUTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;

        }
    }
    bool EnemyIsAlive()
    {
        seachCountdown -= Time.deltaTime;
        if (seachCountdown <= 0f)
        {
            seachCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Inimigo") == null)
            {
                return false;
            }
        }
            return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        PhotonNetwork.InstantiateSceneObject("Enemy", _sp.position, _sp.rotation);
        PhotonNetwork.InstantiateSceneObject("Enemy2", _sp.position, _sp.rotation);
    }
}
