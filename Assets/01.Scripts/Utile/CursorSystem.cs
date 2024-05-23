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
    [SerializeField] private Texture2D _nomalCursor;

    MovefocusMode _currentMode;

    private void OnEnable()
    {
        SignalHub.OnBattleModeChanged += ChangeCursorIcon;
        SignalHub.OnBattlePhaseStartEvent += ChangeCursorIconOnStart;
        SignalHub.OnBattlePhaseEndEvent += ChangeCursorIconToNomal; 
    }

    private void OnDisable()
    {
        SignalHub.OnBattleModeChanged -= ChangeCursorIcon;
        SignalHub.OnBattlePhaseStartEvent -= ChangeCursorIconOnStart;
        SignalHub.OnBattlePhaseEndEvent -= ChangeCursorIconToNomal;
    }
    private void ChangeCursorIcon(MovefocusMode cursorMode)
    {
        if (cursorMode == MovefocusMode.Command && WaveManager.Instance.IsBattlePhase)
        {
            Vector2 cursorOffset = new Vector2(_commandCursor.width / 2, _commandCursor.height / 2);
            Cursor.SetCursor(_commandCursor, cursorOffset, CursorMode.Auto);
        }
        else if(cursorMode == MovefocusMode.Battle && WaveManager.Instance.IsBattlePhase)
        {
            Vector2 cursorOffset = new Vector2(_battleCursor.width / 2, _battleCursor.height / 2);
            Cursor.SetCursor(_battleCursor, cursorOffset, CursorMode.Auto);
        }
        else
        {
            Vector2 cursorOffset = new Vector2(_nomalCursor.width / 2, _nomalCursor.height / 2);
            Cursor.SetCursor(_nomalCursor, cursorOffset, CursorMode.Auto);
        }

        _currentMode = cursorMode;
    }

    private void ChangeCursorIconOnStart()
    {
        if (_currentMode == MovefocusMode.Command)
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

    private void ChangeCursorIconToNomal()
    {
        Vector2 cursorOffset = new Vector2(_nomalCursor.width / 2, _nomalCursor.height / 2);
        Cursor.SetCursor(_nomalCursor, cursorOffset, CursorMode.Auto);
    }
}