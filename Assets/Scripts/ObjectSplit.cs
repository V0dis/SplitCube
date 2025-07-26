using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObjectSplit : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _explosionForceAfterSplit = 20f;
    [SerializeField] private float _explosionForceAfterDestroy = 20f;
    
    private float _newSizeMultiplier = 0.5f;
    private float _newСhanceMultiplier = 0.5f;
    private float _chanceSplit = 1f;

    private void OnMouseDown()
    {
        if (Random.value <= _chanceSplit)
        {
            CreateClones();
            Explode(_explosionForceAfterSplit, _prefab.transform.localScale.x / 2);
        }
        else
        {
            Explode(_explosionForceAfterDestroy, _prefab.transform.localScale.x * 10);
        }
        
        Destroy(gameObject);
    }

    private void CreateClones()
    {
        int minCount = 2;
        int maxCount = 6;
        int count = Random.Range(minCount, maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            CreateSingleClone();
        }
    }
    
    private void CreateSingleClone()
    {
        var radiusSpawn = transform.localScale.x / 2;
        
        Vector3 randomSpawnPoint = Random.insideUnitSphere * radiusSpawn + transform.position;
        GameObject clone = Instantiate(_prefab, randomSpawnPoint, Quaternion.identity);
        
        clone.transform.localScale *= _newSizeMultiplier;
        clone.GetComponent<Renderer>().material.color = Random.ColorHSV();
        clone.GetComponent<ObjectSplit>()._chanceSplit *= _newСhanceMultiplier;
        clone.GetComponent<ObjectSplit>()._explosionForceAfterSplit *= _newSizeMultiplier;
    }

    public void Explode(float explosionForce, float explosionRadius)
    {
        Collider[] hittedClones = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hittedClone in hittedClones)
        {
            Rigidbody attachedNewClone = hittedClone.attachedRigidbody;
            
            if (attachedNewClone != null)
            {
                Vector3 direction = hittedClone.transform.position - transform.position;
                attachedNewClone.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
            }
        }
    }
}