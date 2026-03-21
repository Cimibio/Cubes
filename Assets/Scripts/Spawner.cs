using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public List<Cube> SpawnCubes(int count, Cube parentCube, Vector3 newScale, int newChance)
    {
        List<Cube> cubes = new List<Cube>();

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

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    private void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
