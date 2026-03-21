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

    private void OnCubesSpawned(Cube parentCube, List<Cube> spawnedCubes)
    {
        foreach (Cube cube in spawnedCubes)
        {
            if (cube != null)
            {
                cube.ApplyExplosionForce(parentCube.ExplodeForce, parentCube.transform.position, parentCube.ExplodeRadius);
            }
        }

        Destroy(parentCube.gameObject);
    }
}
