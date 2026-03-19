using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _splitChance = 100;
    [SerializeField] private float _explodeRadius = 10f;
    [SerializeField] private float _explodeForce = 2000f;

    public static event Action<Cube> Clicked;
    public static event Action<Cube> Exploded;

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

    public void Explode()
    {
        foreach (Rigidbody affectedObjects in GetAffectedObjects())
            affectedObjects.AddExplosionForce(_explodeForce, transform.position, _explodeRadius);

        Exploded?.Invoke(this);
    }

    private List<Rigidbody> GetAffectedObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null && hit.attachedRigidbody.transform.IsChildOf(transform))
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
        //Explode();
    }
}
