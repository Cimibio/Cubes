using System.Collections.Generic;
using UnityEngine;



public class CubeDestroyer : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.CubesSpawned += OnCubesSpawned;
    }

    private void OnDisable()
    {
        _spawner.CubesSpawned -= OnCubesSpawned;
    }

    private void DestroyCube(Cube parentCube)
    {        
        Destroy(parentCube.gameObject);
    }

    private void OnCubesSpawned(Cube parentCube, List<Cube> spawnedCubes)
    {
        Destroy(parentCube.gameObject);

        foreach (Cube cube in spawnedCubes)
        {
            if (cube != null)
            {
                cube.ApplyExplosionForce(parentCube.ExplodeForce, parentCube.transform.position, parentCube.ExplodeRadius);
            }
        }
    }
}
