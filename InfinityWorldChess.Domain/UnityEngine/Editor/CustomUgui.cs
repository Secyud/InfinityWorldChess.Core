#if UNITY_EDITOR

#region

using InfinityWorldChess.RoleDomain;
using UnityEditor;
using UnityEditor.UI;

#endregion

namespace InfinityWorldChess.UnityEngine.Editor
{
	[CustomEditor(typeof(AvatarElementXySrImage), true), CanEditMultipleObjects]
	public class AvatarElementXySrImageEditor : ImageEditor
	{
		private SerializedProperty _rangeX;
		private SerializedProperty _rangeY;
		private SerializedProperty _sizeRangeMax;
		private SerializedProperty _sizeRangeMin;
		private SerializedProperty _rotationMin;
		private SerializedProperty _rotationMax;

		protected override void OnEnable()
		{
			base.OnEnable();
			_rangeX = serializedObject.FindProperty("RangeX");
			_rangeY = serializedObject.FindProperty("RangeY");
			_sizeRangeMax = serializedObject.FindProperty("SizeRangeMax");
			_sizeRangeMin = serializedObject.FindProperty("SizeRangeMin");
			_rotationMin = serializedObject.FindProperty("RotationMin");
			_rotationMax = serializedObject.FindProperty("RotationMax");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			serializedObject.Update();
			EditorGUILayout.PropertyField(_rangeX);
			EditorGUILayout.PropertyField(_rangeY);
			EditorGUILayout.PropertyField(_sizeRangeMax);
			EditorGUILayout.PropertyField(_sizeRangeMin);
			EditorGUILayout.PropertyField(_rotationMin);
			EditorGUILayout.PropertyField(_rotationMax);
			serializedObject.ApplyModifiedProperties();
		}
	}

	[CustomEditor(typeof(AvatarElementSyImage), true), CanEditMultipleObjects]
	public class AvatarElementXImageEditor : ImageEditor
	{
		private SerializedProperty _sizeRangeMax;
		private SerializedProperty _sizeRangeMin;
		private SerializedProperty _rangeY;

		protected override void OnEnable()
		{
			base.OnEnable();
			_sizeRangeMax = serializedObject.FindProperty("SizeRangeMax");
			_sizeRangeMin = serializedObject.FindProperty("SizeRangeMin");
			_rangeY = serializedObject.FindProperty("RangeY");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			serializedObject.Update();
			EditorGUILayout.PropertyField(_sizeRangeMax);
			EditorGUILayout.PropertyField(_sizeRangeMin);
			EditorGUILayout.PropertyField(_rangeY);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif