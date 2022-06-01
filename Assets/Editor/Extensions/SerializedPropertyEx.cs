using System.Reflection;
using UnityEditor;

namespace TopDown.Scripts.Base
{
    public static class SerializedPropertyEx
    {
        public static T GetValue<T>(this SerializedProperty property) where T : class
        {
            object value = property.serializedObject.targetObject;

            foreach (var propertyName in property.propertyPath.Split('.'))
            {
                var fieldInfo = value.GetType()
                    .GetField(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
                value = fieldInfo.GetValue(value);
            }

            return value as T;
        }
    }
}
