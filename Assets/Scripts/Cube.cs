using UnityEngine;

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    
    public float SplitChance { get; private set; } = 1f;
    
    private void Awake()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
    }
    
    public void Initialize(float splitChance, Color color)
    {
        SplitChance = splitChance;
        _renderer.material.color = color;
    }
}