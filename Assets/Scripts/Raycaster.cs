using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public RaycastHit? GetHitInfo()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log($"Clicked object: {hit.collider.name}");
            return hit;
        }

        return null;
    }
}
