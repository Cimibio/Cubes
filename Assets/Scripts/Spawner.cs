using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private int _chanceDivider = 2;
    [SerializeField] private int _scaleDivider = 2;

    private int _minRandomChance = 0;
    private int _maxRandomChance = 100;

    private void OnEnable()
    {
        Cube.Clicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        Cube.Clicked -= OnCubeClicked;
    }

    private void OnCubeClicked(Cube clickedCube)
    {
        float randomValue = UnityEngine.Random.Range(_minRandomChance, _maxRandomChance + 1);

        if (randomValue <= clickedCube.SplitChance)
        {
            int count = UnityEngine.Random.Range(_minSpawnCount, _maxSpawnCount + 1);
            SpawnCubes(count, clickedCube);
        }

        clickedCube.Explode();
    }

    private void SpawnCubes(int count, Cube parentCube)
    {
        List<Cube> spawnedCubes = new();

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, parentCube.transform.position, parentCube.transform.rotation);

            int newChance = parentCube.SplitChance / _chanceDivider;
            Vector3 newScale = parentCube.transform.localScale / _scaleDivider;

            newCube.Init(newChance, newScale);
        }        
    }
}
