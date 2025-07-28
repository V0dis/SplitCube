using System.Collections.Generic;
using UnityEngine;

public class ObjectSplit : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _explosionForce = 20f;
    
    [Header("Split setting")]
    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private float _splitChanceMultiplier = 0.5f;
    [SerializeField, Min(0)] private int _minClones = 2;
    [SerializeField, Min(0)] private int _maxClones = 6;

    private float _currentSplitChance = 1f;

    private void OnMouseDown()
    {
        if (Random.value <= _currentSplitChance)
        {
            List<GameObject> clones = new List<GameObject>();
            
            CreateClones(clones);
            Explode(clones);
        }
        
        Destroy(gameObject);
    }

    private void CreateClones(List<GameObject> clones)
    {
        if (_maxClones < _minClones)
            _maxClones = _minClones;
        
        int count = Random.Range(_minClones, _maxClones + 1);
        float spawnRadius = transform.localScale.x / 2;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject clone = Instantiate(_prefab, spawnPoint, Quaternion.identity);
            
            clones.Add(clone);
            
            InitializeClone(clone);
        }
    }

    private void InitializeClone(GameObject clone)
    {
        clone.transform.localScale = transform.localScale * _sizeMultiplier;
        
        Renderer renderer = clone.GetComponent<Renderer>();
        
        if (renderer != null)
            renderer.material.color = Random.ColorHSV();
        
        ObjectSplit splitScript = clone.GetComponent<ObjectSplit>();
        splitScript._currentSplitChance = _currentSplitChance * _splitChanceMultiplier;
        splitScript._explosionForce = _explosionForce * _sizeMultiplier;
    }

    private void Explode(List<GameObject> clones)
    {
        foreach (GameObject clone in clones)
        {
            Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
            
            if (rigidbody != null)
            {
                Vector3 direction = (clone.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * _explosionForce, ForceMode.Impulse);
            }
        }
    }
}