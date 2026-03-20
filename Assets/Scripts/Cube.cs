using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _splitChance = 100;
    [SerializeField] private float _explodeRadius = 10f;
    [SerializeField] private float _explodeForce = 200f;

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

    public void ApplyExplosionForce(float force, Vector3 position, float radius)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(force, position, radius);
        }
    }

    private void Explode()
    {
        foreach (Rigidbody affectedObjects in GetAffectedObjects())
            affectedObjects.AddExplosionForce(_explodeForce, transform.position, _explodeRadius);
    }

    private List<Rigidbody> GetAffectedObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null )
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
