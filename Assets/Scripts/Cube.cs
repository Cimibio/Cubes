using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _splitChance = 100;
    [SerializeField] private float _explodeRadius = 0f;
    [SerializeField] private float _explodeForce = 0f;

    public static event Action<Cube> Clicked;
    public static event Action<Cube> Exploded;

    public int SplitChance => _splitChance;

    public void ReduceSplitChance(int divider)
    {
        _splitChance = _splitChance / divider;
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
        Explode();
    }
}
