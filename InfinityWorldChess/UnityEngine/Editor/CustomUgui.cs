#if UNITY_EDITOR

#region

using InfinityWorldChess.RoleDomain;
using UnityEditor;
using UnityEngine.Editor;

#endregion

namespace InfinityWorldChess.UnityEngine.Editor
{
    [CustomEditor(typeof(AvatarElementImage))]
    public class AvatarElementImageEditor : SImageEditor
    {
        private SerializedProperty _editor;
        private SerializedProperty _elementType;

        protected override void OnEnable()
        {
            base.OnEnable();
            _editor = serializedObject.FindProperty("Editor");
            _elementType = serializedObject.FindProperty("ElementType");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_editor);
            EditorGUILayout.PropertyField(_elementType);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif