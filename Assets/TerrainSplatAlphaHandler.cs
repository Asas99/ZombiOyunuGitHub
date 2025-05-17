using UnityEngine;

public class TerrainSplatAlphaHandler : MonoBehaviour
{
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            foreach (var alphamapTextures in terrain.terrainData.alphamapTextures)
            {
                if (alphamapTextures != null)
                {
                    alphamapTextures.ignoreMipmapLimit = true;
                }
            }
        }
    }
}