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
            Vector2 cursorOffset = new Vector2(_commandCursor.width / 2, _commandCursor.height / 2);
            Cursor.SetCursor(_commandCursor, cursorOffset, CursorMode.Auto);
        }
        else
        {
            Vector2 cursorOffset = new Vector2(_battleCursor.width / 2, _battleCursor.height / 2);
            Cursor.SetCursor(_battleCursor, cursorOffset, CursorMode.Auto);
        }
    }
}