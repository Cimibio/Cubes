using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private void OnEnable()
    {
        Cube.Exploded += DestroyCube;
    }

    private void OnDisable()
    {
        Cube.Exploded -= DestroyCube;
    } 

    private void DestroyCube(Cube parentCube)
    {
        foreach (Transform child in parentCube.transform)
        {
            child.SetParent(null);
        }

        Destroy(parentCube.gameObject);
    }
}
