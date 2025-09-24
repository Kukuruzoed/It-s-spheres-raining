using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spherePrefab;

    [Header("Spawn settings")]
    [Range(0.1f, 10f)]
    public float spawnRate = 1f;

    public int maxActiveSpheres = 200;

    private Coroutine spawnCoroutine;

    void OnEnable()
    {
        StartSpawning();
    }


    void OnDisable()
    {
        StopSpawning();
    }


    public void StartSpawning()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnLoop());
    }


    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            int active = GameObject.FindGameObjectsWithTag("Sphere").Length;
            if (spherePrefab != null && active < maxActiveSpheres)
            {
                GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
                sphere.transform.localScale = Vector3.one * Random.Range(0.1f, 0.4f);
            }

            float interval = Mathf.Clamp(1f / Mathf.Max(0.0001f, spawnRate), 0.01f, 10f);
            yield return new WaitForSeconds(interval);
        }
    }

    public void SetSpawnRate(float rate)
    {
        spawnRate = Mathf.Clamp(rate, 0.1f, 10f);
    }
}
