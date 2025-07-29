using UnityEngine;

public class ObjectValues : MonoBehaviour
{
    public float SplitChance { get; private set; } = 1f;

    public void SetNewChance(float newSplitChance)
    {
        SplitChance = newSplitChance;
    }
    
}
