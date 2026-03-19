using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
