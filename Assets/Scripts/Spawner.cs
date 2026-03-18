using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

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
        float randomValue = UnityEngine.Random.Range(0, 101);

        if (randomValue <= clickedCube.SplitChance)
        {
            int count = UnityEngine.Random.Range(2, 7);
            SpawnCubes(count, clickedCube);
        }

        Destroy(clickedCube.gameObject);
    }

    private void SpawnCubes(int count, Cube parentCube)
    {
        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, parentCube.transform.position, Quaternion.identity);

            int newChance = parentCube.SplitChance / 2;
            Vector3 newScale = parentCube.transform.localScale / 2f;

            newCube.Init(newChance, newScale);
        }
    }
}
