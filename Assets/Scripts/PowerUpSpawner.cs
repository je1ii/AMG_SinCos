using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 3f;
    public int maxPowerUps = 2;
    public Vector2 spawnArea = new Vector2(7f, 4f);

    private int currentPowerUps = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentPowerUps < maxPowerUps)
            {
                SpawnPowerUp();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPowerUp()
    {
        float x = Random.Range(-spawnArea.x, spawnArea.x);
        float y = Random.Range(-spawnArea.y, spawnArea.y);
        Vector2 spawnPos = new Vector2(x, y);

        GameObject pu = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        currentPowerUps++;

        pu.GetComponent<PowerUp>().SetSpawner(this);
    }

    public void PowerUpCollected()
    {
        currentPowerUps = Mathf.Max(currentPowerUps - 1, 0);
    }
}
