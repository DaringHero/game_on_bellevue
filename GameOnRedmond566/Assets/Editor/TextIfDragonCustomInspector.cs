using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextIfDragon)), CanEditMultipleObjects]
public class CustomInspectorTextIfDragon : Editor {

	public SerializedProperty replaceWithProp;

	public void OnEnable()
	{
		this.replaceWithProp = serializedObject.FindProperty("ReplaceWith");
	}

	public override void OnInspectorGUI() 
	{
		base.OnInspectorGUI();
    	 serializedObject.Update ();
         replaceWithProp.stringValue = EditorGUILayout.TextArea( replaceWithProp.stringValue, GUILayout.MaxHeight(75) );
         serializedObject.ApplyModifiedProperties ();
	}

}
