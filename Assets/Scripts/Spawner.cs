using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Cube Settings")] 
    [SerializeField] private SplitSetting _cubeSetting;
    
    [Header("Spawn Count")] 
    [SerializeField] private int _minSpawn;
    [SerializeField] private int _maxSpawn;
    [SerializeField] private Raycaster _raycaster;

    private GameObject _parentObject;
        
    public event Action<GameObject> IsSpawned;

    private void OnEnable()
    {
        _raycaster.IsRaycastOnObject += CheckCance;
    }

    private void OnDisable()
    {
        _raycaster.IsRaycastOnObject -= CheckCance;
    }

    private void CheckCance(GameObject parentObject)
    {
        float chanceSplit = parentObject.GetComponent<ObjectValues>().SplitChance;

        if (Random.value <= chanceSplit)
        {
            _parentObject = parentObject;
            SpawnObjects();
        }
        
        Destroy(parentObject);
    }
    
    private void SpawnObjects()
    {
        int count = Random.Range(_minSpawn, _maxSpawn + 1);
        float spawnRadius = _parentObject.transform.localScale.x / 2;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPoint = _parentObject.transform.localPosition + Random.insideUnitSphere * spawnRadius;
        
            var clone = Instantiate(_parentObject, spawnPoint, Quaternion.identity);
            
            InitializeSingleObject(clone);
        }
        IsSpawned?.Invoke(_parentObject);
    }

    private void InitializeSingleObject(GameObject clone)
    {
        clone.transform.localScale *= _cubeSetting.SizeMultipilier;
        
        Renderer renderer = clone.GetComponent<Renderer>();
        
        if (renderer != null)
            renderer.material.color = Random.ColorHSV();

        float parentSplitChance = _parentObject.GetComponent<ObjectValues>().SplitChance;
        float newSplitChance = parentSplitChance * _cubeSetting.ChanceMultiplier;
        clone.GetComponent<ObjectValues>().SetNewChance(newSplitChance);
    }
}
