using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject FoodPrefab;
    public BoxCollider spawnArea;
    public float spawnTimer = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InvokeRepeating(nameof(SpawnFood), 1f, spawnTimer);

    }

    void SpawnFood (){
        Vector3 spawnPosition = GetRandomPointInBounds(spawnArea.bounds);
        Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.center.y; // You can adjust this based on your level
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
  
}
