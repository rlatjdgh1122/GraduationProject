using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CursorSystem : MonoBehaviour
{
    [SerializeField] private Texture2D _commandCursor;
    [SerializeField] private Texture2D _battleCursor;

    private void Awake()
    {
        SignalHub.OnBattleModeChanged += ChangeCursorIcon;
    }

    private void ChangeCursorIcon(MovefocusMode cursorMode)
    {
        if (cursorMode == MovefocusMode.Command)
        {
            Cursor.SetCursor(_commandCursor, new Vector2(_commandCursor.width / 3, 0), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(_battleCursor, new Vector2(_battleCursor.width / 3, 0), CursorMode.Auto);
        }
    }
}