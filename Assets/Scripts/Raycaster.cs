using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;
    
    public event Action<Cube> IsRaycastCube; 

    private void OnEnable()
    {
        _inputReader.OnInputEvent += HandleInput;
    }

    private void OnDisable()
    {
        _inputReader.OnInputEvent -= HandleInput;
    }

    private void HandleInput(Vector3 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                IsRaycastCube?.Invoke(cube);
            }
        }
    }
}