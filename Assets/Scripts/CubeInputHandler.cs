using System.Collections.Generic;
using UnityEngine;

public class CubeInputHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private MouseReader _mouseReader;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CubeExploder _cubeExploder;

    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private int _chanceDivider = 2;
    [SerializeField] private int _scaleDivider = 2;

    private int _minRandomChance = 0;
    private int _maxRandomChance = 100;

    private void OnEnable()
    {
        _mouseReader.MouseClicked += SelectCube;
    }

    private void OnDisable()
    {
        _mouseReader.MouseClicked -= SelectCube;
    }

    private void SelectCube()
    {
        var hitInfo = _raycaster.GetHitInfo();

        if (hitInfo.HasValue)
            if (hitInfo.Value.collider.TryGetComponent(out Cube cube))
                CubeClick(cube);
    }

    private void CubeClick(Cube clickedCube)
    {
        List<Cube> spawnedCubes = new List<Cube>();

        if (TrySpawnCubes(clickedCube, out spawnedCubes))
            _cubeExploder.ExplodeCubes(clickedCube, spawnedCubes);

        _spawner.DestroyCube(clickedCube);
    }

    public bool TrySpawnCubes(Cube clickedCube, out List<Cube> spawnedCubes)
    {
        float randomValue = Random.Range(_minRandomChance, _maxRandomChance + 1);

        if (randomValue <= clickedCube.SplitChance)
        {
            Vector3 newScale = clickedCube.transform.localScale / _scaleDivider;
            int newChance = clickedCube.SplitChance / _chanceDivider;
            int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            spawnedCubes = _spawner.SpawnCubes(count, clickedCube, newScale, newChance);

            return true;
        }
        else
        {
            spawnedCubes = null;

            return false;
        }
    }
}
