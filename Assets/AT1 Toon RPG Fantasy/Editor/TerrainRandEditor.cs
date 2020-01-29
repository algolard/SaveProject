using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TerrainRand))]
public class TerrainRandEditor : Editor
{
	private float minScale = 0.5f;
	private float maxScale = 1f;
	public GameObject parentGroup;

	override public void OnInspectorGUI()
	{
		TerrainRand terrain = target as TerrainRand;	
		terrain.prefab = EditorGUILayout.ObjectField("Prefab", terrain.prefab, typeof(GameObject)) as GameObject;
		terrain.count = EditorGUILayout.IntField("Count", terrain.count);
		terrain.randomRotationX = EditorGUILayout.FloatField("Random X", terrain.randomRotationX);
		terrain.randomRotationY = EditorGUILayout.FloatField("Random Y", terrain.randomRotationY);
		terrain.randomRotationZ = EditorGUILayout.FloatField("Random Z", terrain.randomRotationZ);
		minScale = EditorGUILayout.FloatField("Min Scale", minScale);
		maxScale = EditorGUILayout.FloatField("Max Scale", maxScale);
		parentGroup = EditorGUILayout.ObjectField("Parent Group", parentGroup, typeof(GameObject), true) as GameObject;


		if (GUILayout.Button("Generate"))
		{
			Generate(terrain);
		}


	}


	
	void Generate(TerrainRand tr)
	{
		if (tr.prefab == null)
		{
			Debug.Log("prefab is null");
			return;
		}
		Transform myTransform = tr.transform;
		Terrain terrain = tr.gameObject.GetComponent<Terrain>();
		TerrainData td = terrain.terrainData;
		for (int i = 0; i < tr.count; i++)
		{
		
			Vector3 pos = myTransform.position;
			pos.x += td.size.x * Random.Range(0.0f, 1.0f);
			pos.z += td.size.z * Random.Range(0.0f, 1.0f);
			pos.y += terrain.SampleHeight(pos);
			Quaternion rot = Quaternion.Euler(Random.Range(-tr.randomRotationX, tr.randomRotationX), Random.Range(-tr.randomRotationY, tr.randomRotationY), Random.Range(-tr.randomRotationZ, tr.randomRotationZ));
			GameObject gameObject = PrefabUtility.InstantiatePrefab(tr.prefab) as GameObject;
			gameObject.transform.position = pos;
			gameObject.transform.rotation = rot;
			float currentScale = Random.Range (minScale, maxScale);
			gameObject.transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
			if (parentGroup != null)
				{
					gameObject.transform.parent = parentGroup.transform;
					
				}

		}		
	}		

}
