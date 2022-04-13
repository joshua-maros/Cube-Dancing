using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawner : MonoBehaviour
{
    public GameObject indicator;
    const int GRID_SIZE = 4;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = -GRID_SIZE; x <= GRID_SIZE; x++) {
            for (int z = -GRID_SIZE; z <= GRID_SIZE; z++) {
                Object.Instantiate(indicator, new Vector3(x + 0.5f, 0, z + 0.5f), Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
