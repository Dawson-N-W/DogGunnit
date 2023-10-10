using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Text roundText;
    public Text enter;
    public int round = 1;
    public int multiplier = 1;
    public int smallEnemyCount =2;
    public int bigEnemyCount = 0;
    public bool roundStarted = false;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject bigEnemyPrefab;

    [SerializeField]
    private float initialEnemyInterval = 3.5f;
    [SerializeField]
    private float initialBigEnemyInterval = 5f;

    private float enemyInterval;
    private float bigEnemyInterval;

    private float timeSinceLastBigEnemy;

    public int totalEnemies;

    
    public GameObject MovePowerUp;
    public GameObject DamagePowerUp;
    public GameObject HealthPowerUp;
    

    // Start is called before the first frame update
    void Start()
    {
        roundText = GameObject.Find("Round").GetComponent<Text>();
        enter = GameObject.Find("Press Enter").GetComponent<Text>();
        enemyInterval = initialEnemyInterval * multiplier;
        bigEnemyInterval = initialBigEnemyInterval * multiplier;

        //StartCoroutine(SpawnEnemies());
    }

  void Update()
{ 
    if (Input.GetKeyDown(KeyCode.Return) && !roundStarted)
    {
        roundStarted = true;
        enter.text = "";
        roundText.text = "Round: " + round;
        StartCoroutine(SpawnEnemies());
    }
}

private IEnumerator SpawnEnemies()
{
    roundStarted = true;

    // Update the values based on the current round
    smallEnemyCount += round;
    bigEnemyCount += round;


    if(enemyInterval > 0.5f)
        enemyInterval -= 0.1f;
    if(bigEnemyInterval > 0.5f)
        bigEnemyInterval -= 0.3f;

    int spawnedEnemies = 0;
    int spawnedBigEnemies = 0;
    totalEnemies = smallEnemyCount + bigEnemyCount;

    while (spawnedEnemies < smallEnemyCount || spawnedBigEnemies < bigEnemyCount)
{

    if (spawnedBigEnemies < bigEnemyCount && Time.time % bigEnemyInterval < Time.deltaTime)
    {
        SpawnEnemy(bigEnemyPrefab);
        spawnedBigEnemies++;
        
        timeSinceLastBigEnemy = 0f;
    }

    if (spawnedEnemies < smallEnemyCount && Time.time % enemyInterval < Time.deltaTime)
    {
        SpawnEnemy(enemyPrefab);
        
        spawnedEnemies++;
    }

    

    timeSinceLastBigEnemy += Time.deltaTime;

    yield return null;
}

    round++;

    roundStarted = false;
}




    private void SpawnEnemy(GameObject enemy)
    {
        Camera mainCamera = Camera.main;
        
       
        if (mainCamera != null)
        {
            float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float cameraHeight = mainCamera.orthographicSize;

            // Calculate random position along perimeter of camera viewport
            float randomSide = Random.Range(0, 4);
            float x, y;
            if (randomSide == 0) // Top
            {
                x = Random.Range(-cameraWidth, cameraWidth);
                y = cameraHeight;
            }
            else if (randomSide == 1) // Right
            {
                x = cameraWidth - 1.5f;
                y = Random.Range(-cameraHeight, cameraHeight);
            }
            else if (randomSide == 2) // Bottom
            {
                x = Random.Range(-cameraWidth, cameraWidth);
                y = -cameraHeight + 1;
            }
            else // Left
            {
                x = -cameraWidth - 0.5f;
                y = Random.Range(-cameraHeight, cameraHeight);
            }

            Vector3 spawnPos = new Vector3(x, y, mainCamera.nearClipPlane);

            GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }

    public void EnemyDestroyed(Vector3 location)
    {
        this.totalEnemies--;
        Debug.Log("Total enemies: " + this.totalEnemies);
        if (this.totalEnemies <= 0)
        {
            enter.text = "Press Enter to start round " + round;
        }

        //10% chance to drop power up at location
        if (Random.Range(0, 3) == 0)
        {
            int powerUp = Random.Range(0, 3);
            if (powerUp == 0)
            {
                Instantiate(MovePowerUp, location, Quaternion.identity);
            }
            else if (powerUp == 1)
            {
                Instantiate(DamagePowerUp, location, Quaternion.identity);
            }
            else
            {
                Instantiate(HealthPowerUp, location, Quaternion.identity);
            }
        }
    }
}
