using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minCount = 2;
    [SerializeField] private int _maxCount = 6;
    [SerializeField] private CubeSplitHandler _cubeSplitHandler;

    private List<Cube> _newCubes = new List<Cube>();

    public List<Cube> GiveCubes()
    {
        List<Cube> newCubes = new List<Cube>(_newCubes);
        
        _newCubes.Clear();
        
        return newCubes;
    }
    
    public void SpawnCubes(Cube clickedCube)
    {
        int count = Random.Range(_minCount, _maxCount + 1);

        for (int i = 0; i < count; i++)
        {
            float spawnRadius = clickedCube.gameObject.transform.localScale.x / 2;
            
            Vector3 spawnPoint = clickedCube.gameObject.transform.localPosition + Random.insideUnitSphere * spawnRadius;
            
            SpawnSingleCube(spawnPoint, clickedCube);
        }
    }

    private void SpawnSingleCube(Vector3 spawnPosition, Cube clickedCube)
    {
        GameObject newObjectCube = Instantiate(clickedCube.gameObject, spawnPosition, Quaternion.identity);
        
        Cube newCube = newObjectCube.GetComponent<Cube>();
        
        newObjectCube.transform.localScale *= _cubeSplitHandler.SizeMultiplier;

        float parentSplitChance = newCube.GetComponent<Cube>().SplitChance;
        float newSplitChance = parentSplitChance * _cubeSplitHandler.ChanceMultiplier;

        newCube.Initialize(newSplitChance, Random.ColorHSV());

        _newCubes.Add(newCube);
    }
}