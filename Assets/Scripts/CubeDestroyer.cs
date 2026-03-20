using UnityEngine;



public class CubeDestroyer : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.CubesSpawned += DestroyCube;
    }

    private void OnDisable()
    {
        _spawner.CubesSpawned -= DestroyCube;
    }

    private void DestroyCube(Cube parentCube)
    {        
        Destroy(parentCube.gameObject);
    }
}
