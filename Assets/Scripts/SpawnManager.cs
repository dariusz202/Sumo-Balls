using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] typeOfBoost = new GameObject[4];
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            gameManager.UpdateWaveNumber(waveNumber);
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
    }
    Vector3 RandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;

    }
    void SpawnEnemyWave(int numberOfEnemies)
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, RandomPosition(), enemyPrefab.transform.rotation);
        }
        int randomIndex = Random.Range(0, typeOfBoost.Length);
        Instantiate(typeOfBoost[randomIndex], RandomPosition(), typeOfBoost[randomIndex].transform.rotation);

    }

}
