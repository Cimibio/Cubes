using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public void DestroyCube(Cube parentCube)
    {
        Debug.Log("Destroy!");
        Destroy(parentCube.gameObject);
    }
}
