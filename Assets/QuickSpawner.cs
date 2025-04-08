using System.Collections.Generic;
using UnityEngine;

public class QuickSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> spawnables;

    float timer;
    public float maxTime = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0)
        {
            float X = Random.Range(-100, 100);
            float Z = Random.Range(-100, 100);

            var go = Instantiate(spawnables[Random.Range(0, spawnables.Count)]);
            go.transform.position = new Vector3(X, 0, Z);

            timer = maxTime;
        }
        timer -= Time.deltaTime;
    }
}
