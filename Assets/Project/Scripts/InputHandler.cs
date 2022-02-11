using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action onClickEvent;

    private bool inputEnable;

    public void EnableInput()
    {
        inputEnable = true;
    }

    public void DisableInput()
    {
        inputEnable = false;
    }
    private void Update()
    {
        if (inputEnable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                onClickEvent?.Invoke();
            }
        }
    }
}
