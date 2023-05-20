using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoSingleton<EnvironmentManager>
{
    [SerializeField] private GameObject environmentPrefab;

    private PlayerMovement player;
    private GameObject lastEnvironment;
    private int spawnCount = 0;
    private void Start()
    {
        player = PlayerMovement.Instance;
        CreateEnvironmentPrefab();
    }
    public void CreateEnvironmentPrefab()
    {
        lastEnvironment = Instantiate(environmentPrefab, transform.position + Helper.Help(0, 0, spawnCount * 100f), Quaternion.identity);
        spawnCount++;
    }
    private void Update()
    {
        if(player.transform.position.z > (lastEnvironment.transform.position.z / 2f))
        {
            CreateEnvironmentPrefab();
        }
    }
}
