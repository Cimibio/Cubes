using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private CubeDestroyer _destroyer;
    [SerializeField] private CubeSelector _selector;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private int _chanceDivider = 2;
    [SerializeField] private int _scaleDivider = 2;

    private int _minRandomChance = 0;
    private int _maxRandomChance = 100;

    public event Action<Cube, List<Cube>> CubesSpawned;

    private void OnEnable()
    {
        _selector.CubeSelected += OnCubeClicked;
    }

    private void OnDisable()
    {
        _selector.CubeSelected -= OnCubeClicked;
    }

    private void OnCubeClicked(Cube clickedCube)
    {
        List<Cube> spawnedCubes = new List<Cube>();

        float randomValue = UnityEngine.Random.Range(_minRandomChance, _maxRandomChance + 1);

        if (randomValue <= clickedCube.SplitChance)
        {
            int count = UnityEngine.Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            spawnedCubes = SpawnCubes(count, clickedCube, _scaleDivider, _chanceDivider);
        }

        CubesSpawned?.Invoke(clickedCube, spawnedCubes);
    }

    private List<Cube> SpawnCubes(int count, Cube parentCube, int scaleDivider, int chanceDivider)
    {
        List<Cube> cubes = new List<Cube>();

        Vector3 newScale = parentCube.transform.localScale / scaleDivider;
        int newChance = parentCube.SplitChance / chanceDivider;

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, parentCube.transform.position,
                                       parentCube.transform.rotation);

            newCube.Init(newChance, parentCube.ExplodeRadius, parentCube.ExplodeForce, newScale);

            SetRandomColor(newCube);
            cubes.Add(newCube);
        }

        return cubes;
    }

    private void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = UnityEngine.Random.ColorHSV();
        }
    }
}
