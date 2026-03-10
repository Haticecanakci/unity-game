using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    static int _livingZombies = 0;
    public static void OnEnemyDeath() => _livingZombies--;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnDelay = 3f;
    [SerializeField] int enemyLimit = 30;
    [SerializeField] LayerMask spawnLayer;

    float nextSpawnTime = -1f;

    void Update()
    {
        if (GameManager.HasPlayerWon)
        {
            Destroy(this);
            return;
        }

        if (Time.time < nextSpawnTime) return;
        if (_livingZombies >= enemyLimit) return;

        TrySpawn();
    }

    void TrySpawn()
    {
        Vector3 edge = new Vector3(1.25f, Random.value, 8f);
        if (Random.value > 0.5f) edge.x = -0.25f;

        Ray ray = Camera.main.ViewportPointToRay(edge);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, spawnLayer))
        {
            Instantiate(enemyPrefab, hit.point, Quaternion.identity);
            _livingZombies++;
            nextSpawnTime = Time.time + spawnDelay;
        }
        else
        {
            nextSpawnTime = Time.time + 0.5f;
        }
    }
}
