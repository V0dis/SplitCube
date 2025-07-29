using UnityEngine;

public class SplitSetting : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _chanceMultiplier = 0.5f;
    [SerializeField] private float _sizeMultiplier = 0.5f;

    public float ChanceMultiplier => _chanceMultiplier;
    public float SizeMultipilier => _sizeMultiplier;
}
