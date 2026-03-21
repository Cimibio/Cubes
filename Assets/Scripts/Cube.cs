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
}
