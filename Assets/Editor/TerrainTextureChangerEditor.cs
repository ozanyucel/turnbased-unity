using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TerrainTextureChanger))]
public class TerrainTextureChangerEditor : Editor {
	
	public override void OnInspectorGUI()
	{
		TerrainTextureChanger changer = (TerrainTextureChanger)target;

		changer.terrain = (Terrain) EditorGUILayout.ObjectField ("Terrain", changer.terrain, typeof(Terrain), true);
		changer.ratio = EditorGUILayout.Slider ("Ratio", changer.ratio, 0, 1);
	}
}
