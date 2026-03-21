using System;
using UnityEngine;

public class ClickProcessor : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private MouseReader _mouseReader;

    public event Action<Cube> CubeSelected;

    private void OnEnable()
    {
        _mouseReader.MouseClicked += SelectCube;
    }

    private void OnDisable()
    {
        _mouseReader.MouseClicked -= SelectCube;
    }

    private void SelectCube()
    {
        var hitInfo = _raycaster.GetHitInfo();

        if (hitInfo.HasValue)
            if (hitInfo.Value.collider.TryGetComponent(out Cube cube))
                CubeSelected?.Invoke(cube);
    }
}
