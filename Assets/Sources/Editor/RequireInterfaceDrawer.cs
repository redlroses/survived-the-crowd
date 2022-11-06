using System;
using UnityEditor;
using UnityEngine;

namespace Sources.Editor
{
    [CustomPropertyDrawer(typeof(RequireInterface))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RequireInterface requireInterface = attribute as RequireInterface;
            Type requireType = requireInterface.RequireType;

            if (IsValid(property, requireType))
            {
                label.tooltip = "Require " + requireInterface.RequireType + " interface";
                CheckProperty(property, requireType);
            }

            EditorGUI.PropertyField(position, property, label);
        }

        private bool IsValid(SerializedProperty property, Type targetType) =>
            targetType.IsInterface && property.propertyType == SerializedPropertyType.ObjectReference;

        private void CheckProperty(SerializedProperty property, Type targetType)
        {
            if (property.objectReferenceValue == null)
                return;

            if (property.objectReferenceValue as GameObject)
                CheckGameObject(property, targetType);
            else if (property.objectReferenceValue as ScriptableObject)
                CheckScriptableObject(property, targetType);
        }

        private void CheckGameObject(SerializedProperty property, Type targetType)
        {
            GameObject field = property.objectReferenceValue as GameObject;

            if (field.GetComponent(targetType) == null)
            {
                property.objectReferenceValue = null;
                Debug.LogError("GameObject must contain component implemented " + targetType + " interface");
            }
        }

        private void CheckScriptableObject(SerializedProperty property, Type targetType)
        {
            ScriptableObject field = property.objectReferenceValue as ScriptableObject;
            Type fieldType = field.GetType();

            if (targetType.IsAssignableFrom(fieldType) == false)
            {
                property.objectReferenceValue = null;
                Debug.LogError("ScriptableObject must implement " + targetType + " interface");
            }
        }
    }
}