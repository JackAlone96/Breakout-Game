using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject[] powerupList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPowerup(Transform reference)
    {
        int probability = Random.Range(0, 10);
        if (probability == 1)
        {
            int powerupIndex = Random.Range(0, powerupList.Length);
            Instantiate(powerupList[powerupIndex], reference.position, Quaternion.identity);
        }

    }
}
