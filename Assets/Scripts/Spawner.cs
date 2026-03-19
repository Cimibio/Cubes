using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public void SpawnCubes(int count, Cube parentCube, int scaleDivider, int chanceDivider)
    {
        Vector3 newScale = Vector3.one / scaleDivider;
        int newChance = parentCube.SplitChance / chanceDivider;

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, parentCube.transform.position,
                                       parentCube.transform.rotation);

            newCube.Init(newChance, parentCube.ExplodeRadius, parentCube.ExplodeForce, newScale);

            Debug.Log($"Spawner create cube#{i + 1}/{count}");
            SetRandomColor(newCube);
        }
    }

    private void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
