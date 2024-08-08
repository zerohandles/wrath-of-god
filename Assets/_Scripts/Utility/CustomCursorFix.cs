using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorFix : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
#if UNITY_WEBGL
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
#else 
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
#endif

    }
}
