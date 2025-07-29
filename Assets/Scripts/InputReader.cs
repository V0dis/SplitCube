using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<Vector3> OnInputEvent;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnInputEvent?.Invoke(Input.mousePosition);
        }
    }
}