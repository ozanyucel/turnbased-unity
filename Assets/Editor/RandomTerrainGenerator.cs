using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class RandomTerrainGenerator : ScriptableWizard {
	
	//The higher the numbers, the more hills/mountains there are
	private float HM = Random.Range(0, 40);
	
	//The lower the numbers in the number range, the higher the hills/mountains will be...
	private float divRange = Random.Range(6,15);
	
	[MenuItem("My Tools/Generate Random Terrain")]
	public static void CreateWizard(MenuCommand command)
	{
		ScriptableWizard.DisplayWizard("Generate Random Terrain", typeof(RandomTerrainGenerator));
	}
	
	void OnWizardCreate()
	{
		GameObject G = Selection.activeGameObject;
		if (G.GetComponent<Terrain>())
		{
			GenerateTerrain(G.GetComponent<Terrain>(), HM);
		}
	}
	
	//Our Generate Terrain function
	public void GenerateTerrain(Terrain t, float tileSize)
	{
		
		//Heights For Our Hills/Mountains
		float[,] hts = new float[t.terrainData.heightmapWidth, t.terrainData.heightmapHeight];
		for (int i = 0; i < t.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < t.terrainData.heightmapHeight; k++)
			{
				hts[i, k] = Mathf.PerlinNoise(((float)i / (float)t.terrainData.heightmapWidth) * tileSize, ((float)k / (float)t.terrainData.heightmapHeight) * tileSize)/ divRange;
			}
		}
		Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
		t.terrainData.SetHeights(0, 0, hts);


		string logStr = "";
		//get current paint mask
		float[, ,] alphas = new float[t.terrainData.alphamapWidth, t.terrainData.alphamapHeight, t.terrainData.alphamapLayers];
		// make sure every grid on the terrain is modified
		for (int i = 0; i < t.terrainData.alphamapWidth; i++)
		{
			for (int k = 0; k < t.terrainData.alphamapHeight; k++)
			{


				float alpha = Mathf.PerlinNoise((float)i / t.terrainData.alphamapWidth * 2, (float)k / t.terrainData.alphamapHeight * 2);
				alphas[i, k, 0] = alpha;
				alphas[i, k, 1] = 1 - alpha;
			}
		}

		//Debug.LogWarning (logStr);

		// apply the new alpha
		t.terrainData.SetAlphamaps(0, 0, alphas);
	}
}