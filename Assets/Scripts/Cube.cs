using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _splitChance = 100;

    public static event Action<Cube> Clicked;

    public int SplitChance => _splitChance;

    private void Start()
    {
        GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }

    public void Init(int newChance, Vector3 newScale)
    {
        _splitChance = newChance;
        transform.localScale = newScale;
    }

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
    }    
}
