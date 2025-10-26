using UnityEngine;

public class CubeSplitHandler : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _chanceMultiplier = 0.5f;
    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    
    private void OnEnable()
    {
        _raycaster.IsRaycastCube += CheckSplitChance;
    }

    private void OnDisable()
    {
        _raycaster.IsRaycastCube -= CheckSplitChance;
    }
    
    public float ChanceMultiplier => _chanceMultiplier;
    public float SizeMultiplier => _sizeMultiplier;

    private void CheckSplitChance(Cube clickedCube)
    {
        if (clickedCube.SplitChance >= Random.Range(0f, 1f))
        { 
            _spawner.SpawnCubes(clickedCube);
            _exploder.Explode(clickedCube.transform.localPosition, _spawner.GiveCubes());
        }

        Destroy(clickedCube.gameObject);
    }
}
