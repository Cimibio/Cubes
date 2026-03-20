using System;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

public class CubeSelector : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField]
    [Tooltip("Выберите кнопку для запуска/остановки таймера: LeftClick/RightClick")]
    private InputButton _primaryActionButton = InputButton.LeftClick;

    public event Action<Cube> CubeSelected;

    private enum InputButton { LeftClick = 0, RightClick = 1 }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)_primaryActionButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent<Cube>(out Cube cube))
                {
                    CubeSelected?.Invoke(cube);
                }
            }
        }
    }
}
