using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] int width = 4;
    [SerializeField] int depth = 4;
    [SerializeField] float scale = 4;
    [SerializeField] List<Material> materialList = new List<Material>();
    [SerializeField] float offsetX;
    [SerializeField] float offsetZ;

    List<GameObject> tiles = new List<GameObject>();

    private void Start()
    {
        //GenerateRandomMap();
        GeneratePerlinMap();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tiles.ForEach(x => Destroy(x.gameObject));
            tiles.Clear();
            GeneratePerlinMap();
        }
    }
    private void GenerateRandomMap()
    {
        for(int x=0; x<width; x++)
        {
            for(int z=0; z<depth; z++)
            {
                var go = Instantiate(tilePrefab, new Vector3(x * 10 , 0, z * 10), Quaternion.identity);
                var ranMaterial = Random.Range(0, materialList.Count);
                SetTileMaterial(go, ranMaterial);
            }
        }
    }
    private void GeneratePerlinMap()
    {
        Debug.Log("..");
        offsetX = Random.Range(0, 5000);
        offsetZ = Random.Range(-5000, 0);
        float xCoord, zCoord;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                xCoord = (float)(x + offsetX) / width * (scale);
                zCoord = (float)(z + offsetZ) / depth * (scale);

                var go = Instantiate(tilePrefab, new Vector3(x * 10, 0, z * 10), Quaternion.identity);
                var ranMaterial = Mathf.Clamp(Mathf.PerlinNoise(xCoord, zCoord), 0, 0.99f);
                SetTileMaterial(go, ranMaterial);

                tiles.Add(go);
            }
        }
    }

    void SetTileMaterial(GameObject go, int index)
    {
        go.GetComponent<Renderer>().material = materialList[index];
    }

    void SetTileMaterial(GameObject go, float random)
    {
        int index = (int)(random * materialList.Count);
        go.GetComponent<Renderer>().material = materialList[index];
        return;

        Material material;

        switch (random)
        {
            case <= .25f:
                material = materialList[0];
                break;
            case <= .5f:
                material = materialList[1];
                break;
            case <= .75f:
                material = materialList[2];
                break;
            case <= 1f:
                material = materialList[3];
                break;

            default:
                material = materialList[0];
                break;
        }

        Debug.Log("random = " + random + "index = " + index);

        go.GetComponent<Renderer>().material = material;
    }
}
