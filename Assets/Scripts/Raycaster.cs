using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _inputReader;
    
    public event Action<GameObject> IsRaycastOnObject; 

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
            IsRaycastOnObject?.Invoke(hit.collider.gameObject);
        }
    }
}
