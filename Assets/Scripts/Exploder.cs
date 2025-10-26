using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    
    public void Explode(Vector3 explosionCenter, List<Cube> newCubes)
    {
        foreach (var cube in newCubes)
        {
            Rigidbody rb = cube.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (cube.transform.position - explosionCenter).normalized;
                rb.AddForce(direction * _explosionForce, ForceMode.Impulse);
            }
        }
    }
}