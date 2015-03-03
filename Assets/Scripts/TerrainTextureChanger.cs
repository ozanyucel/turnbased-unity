using UnityEngine;
using System.Collections;
public class TerrainTextureChanger : MonoBehaviour
{
	public Terrain terrain;
	public float ratio = 1;
	//private float[, ,] baseAlphas;

	void Start()
	{
		//get current paint mask
		//baseAlphas = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			UpdateTerrainTexture(terrain.terrainData, 0);
		}
		if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			UpdateTerrainTexture(terrain.terrainData, 1);
		}
		if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			UpdateTerrainTexture(terrain.terrainData, 2);
		}
	}

	void UpdateTerrainTexture(TerrainData terrainData, int textureNumberTo)
	{
		//get current paint mask
		float[, ,] alphas = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
		// make sure every grid on the terrain is modified
		for (int i = 0; i < terrainData.alphamapWidth; i++)
		{
			for (int j = 0; j < terrainData.alphamapHeight; j++)
			{
				for (int k = 0; k < terrainData.alphamapLayers; k++)
				{

					if (k == textureNumberTo)
						alphas[i, j, k] = ratio;
					else alphas[i, j, k] = (1 - ratio) / (terrainData.alphamapLayers - 1);
				}
			}
		}
		// apply the new alpha
		terrainData.SetAlphamaps(0, 0, alphas);
	}
}