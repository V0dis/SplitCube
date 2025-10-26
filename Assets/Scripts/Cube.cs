using UnityEngine;

public class Cube : MonoBehaviour
{
    public float SplitChance { get; private set; } = 1f;
    public void Initialize(float splitChance, Color color)
    {
        SplitChance = splitChance;
        
        var renderer = GetComponent<Renderer>();
        
        if (renderer != null)
            renderer.material.color = color;
    }
}
