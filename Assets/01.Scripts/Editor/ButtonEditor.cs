using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonEvent))]
public class ButtonEditor : Editor
{
    private ButtonEvent _btnEvent;

    private void OnEnable()
    {
        _btnEvent = (ButtonEvent)target; //타겟 지정
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CustomButtonEvent inspectorObj = _btnEvent.InspectorCustomBtn;

        if (inspectorObj.TweenBtn) //만약 TweenBtn이 켜져있다면
        {
            inspectorObj.Type = (TweenType)EditorGUILayout.EnumPopup("Button Tween Type",
                inspectorObj.Type); //TweenType 이넘을 인스펙터에 보여준다


            switch (inspectorObj.Type)
            {
                case TweenType.Fade:
                {

                        inspectorObj.FadeValue = EditorGUILayout.FloatField("Fade Value", inspectorObj.FadeValue);

                }
                break;


                case TweenType.All:
                {

                        inspectorObj.Direction = (TweenDirection)EditorGUILayout.EnumPopup("Button Tween Direction",
                                inspectorObj.Direction); //TweenDirection 이넘을 인스펙터에 보여준다

                        if (inspectorObj.Direction == TweenDirection.All)
                        {

                            inspectorObj.MoveAll = EditorGUILayout.Vector3Field("Move Vector Value", inspectorObj.MoveAll);
                            inspectorObj.ScaleAll = EditorGUILayout.Vector3Field("Scale Vector Value", inspectorObj.ScaleAll);

                        }

                        else
                        {
                            if (inspectorObj.Direction == TweenDirection.X)
                            {

                                inspectorObj.MoveX = EditorGUILayout.FloatField("X Value", inspectorObj.MoveX); //X면

                            }
                            else
                            {

                                inspectorObj.MoveY = EditorGUILayout.FloatField("Y Value", inspectorObj.MoveY); //Y면

                            }
                        }

                        inspectorObj.FadeValue = EditorGUILayout.FloatField("Fade Value", inspectorObj.FadeValue);
                    }
                break;

                case TweenType.Scale:
                {

                    inspectorObj.Direction = (TweenDirection)EditorGUILayout.EnumPopup("Button Tween Direction",
                            inspectorObj.Direction);

                    switch (inspectorObj.Direction)
                    {
                        case TweenDirection.X:
                            inspectorObj.ScaleX = EditorGUILayout.FloatField("X Value", inspectorObj.ScaleX); //X면
                            break;

                        case TweenDirection.Y:
                            inspectorObj.ScaleY = EditorGUILayout.FloatField("Y Value", inspectorObj.ScaleY); //Y면
                            break;

                        default:
                            inspectorObj.ScaleAll = EditorGUILayout.Vector3Field("Scale Value", inspectorObj.ScaleAll); //All 이면
                            break;
                    }

                }
                break;

                case TweenType.Move:
                {

                    inspectorObj.Direction = (TweenDirection)EditorGUILayout.EnumPopup("Button Tween Direction",
                            inspectorObj.Direction);


                    switch (inspectorObj.Direction)
                    {
                        case TweenDirection.X:
                            inspectorObj.MoveX = EditorGUILayout.FloatField("X Value", inspectorObj.MoveX); //X면
                            break;

                        case TweenDirection.Y:
                            inspectorObj.MoveY = EditorGUILayout.FloatField("Y Value", inspectorObj.MoveY); //Y면
                            break;

                        default:
                            inspectorObj.MoveAll = EditorGUILayout.Vector3Field("Move Value", inspectorObj.MoveAll); //All 이면
                            break;
                    }
                }
                break;
            }


            inspectorObj.Time = EditorGUILayout.FloatField("Duration Time", inspectorObj.Time);
        }
    }
}
