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
        _btnEvent = (ButtonEvent)target; //Ÿ�� ����
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CustomButtonEvent inspectorObj = _btnEvent.InspectorCustomBtn;

        if (inspectorObj.TweenBtn) //���� TweenBtn�� �����ִٸ�
        {
            inspectorObj.Type = (TweenType)EditorGUILayout.EnumPopup("Button Tween Type",
                inspectorObj.Type); //TweenType �̳��� �ν����Ϳ� �����ش�


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
                                inspectorObj.Direction); //TweenDirection �̳��� �ν����Ϳ� �����ش�

                        if (inspectorObj.Direction == TweenDirection.All)
                        {

                            inspectorObj.MoveAll = EditorGUILayout.Vector3Field("Move Vector Value", inspectorObj.MoveAll);
                            inspectorObj.ScaleAll = EditorGUILayout.Vector3Field("Scale Vector Value", inspectorObj.ScaleAll);

                        }

                        else
                        {
                            if (inspectorObj.Direction == TweenDirection.X)
                            {

                                inspectorObj.MoveX = EditorGUILayout.FloatField("X Value", inspectorObj.MoveX); //X��

                            }
                            else
                            {

                                inspectorObj.MoveY = EditorGUILayout.FloatField("Y Value", inspectorObj.MoveY); //Y��

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
                            inspectorObj.ScaleX = EditorGUILayout.FloatField("X Value", inspectorObj.ScaleX); //X��
                            break;

                        case TweenDirection.Y:
                            inspectorObj.ScaleY = EditorGUILayout.FloatField("Y Value", inspectorObj.ScaleY); //Y��
                            break;

                        default:
                            inspectorObj.ScaleAll = EditorGUILayout.Vector3Field("Scale Value", inspectorObj.ScaleAll); //All �̸�
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
                            inspectorObj.MoveX = EditorGUILayout.FloatField("X Value", inspectorObj.MoveX); //X��
                            break;

                        case TweenDirection.Y:
                            inspectorObj.MoveY = EditorGUILayout.FloatField("Y Value", inspectorObj.MoveY); //Y��
                            break;

                        default:
                            inspectorObj.MoveAll = EditorGUILayout.Vector3Field("Move Value", inspectorObj.MoveAll); //All �̸�
                            break;
                    }
                }
                break;
            }


            inspectorObj.Time = EditorGUILayout.FloatField("Duration Time", inspectorObj.Time);
        }
    }
}
