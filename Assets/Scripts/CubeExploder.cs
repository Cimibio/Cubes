using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    public void ExplodeCubes(Cube parentCube, List<Cube> affectedCubes)
    {
        foreach (Cube cube in affectedCubes)
        {
            if (cube != null)
            {
                ApplyExplosionForce(parentCube.ExplodeForce, parentCube.transform.position, parentCube.ExplodeRadius);
            }
        }        
    }
    public void ExplodeCubes(Cube cube)
    {
        foreach (Rigidbody affectedObjects in GetAffectedObjects(cube))
            affectedObjects.AddExplosionForce(cube.ExplodeForce, cube.transform.position, cube.ExplodeRadius);
    }

    private void ApplyExplosionForce(float force, Vector3 position, float radius)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(force, position, radius);
        }
    }

    private List<Rigidbody> GetAffectedObjects(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, cube.ExplodeRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
