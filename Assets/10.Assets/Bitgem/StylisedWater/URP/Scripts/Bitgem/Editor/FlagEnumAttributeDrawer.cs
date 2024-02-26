﻿#region Using statements

using Bitgem.Core;
using UnityEditor;
using UnityEngine;

#endregion

namespace Bitgem.Editor
{
    [CustomPropertyDrawer(typeof(Core.FlagEnumAttribute))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
        }
    }
}