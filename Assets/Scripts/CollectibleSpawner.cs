using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
	public SpriteRenderer floorSprite;
	public GameObject collectiblePrefab;
	public int collectibleCount = 10;
	public LayerMask furnitureLayer;
	public float checkRadius = 0.3f;

	private Vector2 spawnAreaMin;
	private Vector2 spawnAreaMax;

	void Start()
	{
		// Get world bounds of floor
		Bounds bounds = floorSprite.bounds;
		spawnAreaMin = bounds.min;
		spawnAreaMax = bounds.max;

		SpawnCollectibles();
	}

	void SpawnCollectibles()
	{
		int spawned = 0;
		int maxAttempts = collectibleCount * 10;

		while (spawned < collectibleCount && maxAttempts > 0)
		{
			Vector2 randomPos = new Vector2(
				Random.Range(spawnAreaMin.x, spawnAreaMax.x),
				Random.Range(spawnAreaMin.y, spawnAreaMax.y)
			);

			bool isBlocked = Physics2D.OverlapCircle(randomPos, checkRadius, furnitureLayer);

			if (!isBlocked)
			{
				Instantiate(collectiblePrefab, randomPos, Quaternion.identity);
				spawned++;
			}

			maxAttempts--;
		}
	}
}
