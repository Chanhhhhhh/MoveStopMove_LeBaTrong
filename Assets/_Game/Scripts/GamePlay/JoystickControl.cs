using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public static Vector3 direct;

    private Vector3 screen;

    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    private Vector3 startPoint;
    private Vector3 updatePoint;

    // Start is called before the first frame update
    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = MousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            updatePoint = MousePosition;
            direct = (updatePoint - startPoint).normalized;
            direct.z = direct.y;
            direct.y = 0;

        }

        if (Input.GetMouseButtonUp(0))
        {
            direct = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        direct = Vector3.zero;
    }
}
