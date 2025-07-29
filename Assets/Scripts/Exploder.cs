using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 20f;
    [SerializeField] private Spawner _spawner;
    
    private void OnEnable()
    {
        _spawner.IsSpawned += Exploude;
    }

    private void OnDisable()
    {
        _spawner.IsSpawned -= Exploude;
    }
    
    private void Exploude(GameObject parentObject)
    {
        float explosionRadius = parentObject.transform.localScale.x / 2;

        Collider[] colliders = Physics.OverlapSphere(parentObject.transform.localPosition, explosionRadius);

        foreach (var collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            
            if (rigidbody != null)
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * _explosionForce, ForceMode.Impulse);
            }
        }
    }
}
