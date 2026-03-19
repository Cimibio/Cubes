using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _splitChance = 100;
    [SerializeField] private float _explodeRadius = 10f;
    [SerializeField] private float _explodeForce = 200f;

    public static event Action<Cube> Clicked;
    public static event Action<Cube> Exploded;

    public int SplitChance => _splitChance;
    public float ExplodeRadius => _explodeRadius;
    public float ExplodeForce => _explodeForce;

    public void Init(int chance, float radius, float force, Vector3 scale)
    {
        _splitChance = chance;
        _explodeRadius = radius;
        _explodeForce = force;
        transform.localScale = scale;
    }

    private void Explode()
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

        Debug.Log($"affected count: {cubes.Count}");

        return cubes;
    }

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
        Explode();
    }
}
