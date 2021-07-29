using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class LevelManager : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private GameObject groundPrefab;
    [SerializeField]
    private GameObject borderPrefab;
    [SerializeField]
    private GameObject[] chunkPrefabs;
    [SerializeField]
    private float sizeX;
    [SerializeField]
    private float sizeZ = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        float buf = sizeX;
        sizeX = sizeZ;
        sizeZ = buf;
        navMeshSurface = GetComponent<NavMeshSurface>();
        GroundGeneration(sizeX, sizeZ);
        float chunkSizeX = 3; 
        float chunkXStep = chunkSizeX / 2; 
        float padding = 0;
        float startX = transform.position.x - sizeZ / 2;
        for (float x = startX + padding + chunkXStep; x <= sizeX; x += chunkSizeX)
        {
            Vector3 chunkPoint = new Vector3(0, 1, padding + x);
            Instantiate(chunkPrefabs[0], chunkPoint, Quaternion.identity, transform);
        }
        navMeshSurface.BuildNavMesh();
    }

    private void GroundGeneration(float sizeX, float sizeZ)
    {
        var ground = Instantiate(groundPrefab,
            Vector3.zero, 
            Quaternion.identity,
            transform);
        float sizeY = ground.transform.localScale.y;
       
        Vector3 newGroundScale = ground.transform.localScale;
        newGroundScale.x = sizeX;
        newGroundScale.z = sizeZ;
        ground.transform.localScale = newGroundScale;
        ground.transform.position = Vector3.zero;
    }
}
