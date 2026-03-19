using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CubeDestroyer _destroyer;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private int _chanceDivider = 2;
    [SerializeField] private int _scaleDivider = 2;

    private int _minRandomChance = 0;
    private int _maxRandomChance = 100;

    private void OnEnable()
    {
        Cube.Clicked += OnCubeClicked;
        Cube.Exploded += OnCubeExploded;
    }

    private void OnDisable()
    {
        Cube.Clicked -= OnCubeClicked;
        Cube.Exploded -= OnCubeExploded;
    }

    private void OnCubeClicked(Cube clickedCube)
    {
        float randomValue = Random.Range(_minRandomChance, _maxRandomChance + 1);

        Debug.Log($"Chance roll: {randomValue}");

        if (randomValue <= clickedCube.SplitChance)
        {
            int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            Debug.Log($"Spawn count: {count}");
            _spawner.SpawnCubes(count, clickedCube, _scaleDivider, _chanceDivider);
        }
    }

    private void OnCubeExploded(Cube explodedCube)
    {
        Debug.Log($"Children count: {explodedCube.transform.childCount}");

        foreach (Transform child in explodedCube.transform)
        {
            child.SetParent(null);
        }

        _destroyer.DestroyCube(explodedCube);
    }
}
