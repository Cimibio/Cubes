using System;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Ray _ray;

    public event Action<Cube> CubeSelected;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;
            Debug.Log(objectHit);

            if (hit.collider.TryGetComponent<Cube>(out Cube cube))
            {
                CubeSelected?.Invoke(cube);
            }
        }
    }
}
