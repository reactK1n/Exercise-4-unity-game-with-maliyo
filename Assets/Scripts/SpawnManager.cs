using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject enemyPrefab;
	public GameObject powerupPrefab;

	private float spawnRange = 9;
	private int enemyCount;
	private int waveNumber = 1;
	// Start is called before the first frame update
	void Start()
	{
		
		SpawnEnemyWave(waveNumber);
		Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);

	}

	// Update is called once per frame
	void Update()
	{
		enemyCount = FindObjectsOfType<Enemy>().Length;
		if (enemyCount == 0 )
		{
			waveNumber++;
			SpawnEnemyWave(waveNumber);
			Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);

		}
	}

	private Vector3 GenerateSpawnPos()
	{
		var spawnPosX = Random.Range(-spawnRange, spawnRange);
		var spawnPosZ = Random.Range(-spawnRange, spawnRange);
		Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
		return randomPos;
	}

	private void SpawnEnemyWave(int enemyToSpawn)
	{

		for (int i = 0; i < enemyToSpawn; i++)
		{
			Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
		}
	}
}
