using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      internal abstract class BasePropertyDrawer : PropertyDrawer
      {
            private bool init;

            public virtual void Initialize(SerializedProperty property) { }
            public abstract void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label);

            public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                  if (!init)
                  {
                        Initialize(property);
                        init = true;
                  }
                  using (new EditorGUI.PropertyScope(position, label, property))
                  {
                        OnPropertyDraw(position, property, label);
                  }
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);
      }
}