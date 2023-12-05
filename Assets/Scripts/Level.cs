#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace HackedDesign
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform buildingParent;
        [SerializeField] private GameObject startBuildingPrefab;
        [SerializeField] private List<GameObject> buildingPrefabs;
        [SerializeField] private float offset = 100.0f;
        [SerializeField] private float randomOffset = 20.0f;
        [SerializeField] private float heightOffset = 50.0f;

        private float[] rotations = { 0, 90, 180, 270 };

        private void EmptyLevel()
        {
            for (int i = 0; i < buildingParent.childCount; i++)
            {
                Destroy(buildingParent.GetChild(i).gameObject);
            }
        }


        public void Generate(int width, int breadth, int minheight, int maxheight)
        {
            EmptyLevel();
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < breadth; z++)
                {
                    if (x == 0 && z == 0)
                    {
                        Instantiate(startBuildingPrefab, new Vector3(x * offset, 0, z * offset), Quaternion.Euler(0, rotations[Random.Range(0, rotations.Length)], 0), buildingParent);
                    }
                    else
                    {
                        float randomX = Random.Range(0, randomOffset);
                        float randomZ = Random.Range(0, randomOffset);
                        Instantiate(GetRandomBuildingPrefab(), new Vector3(x * offset + randomX, 0, z * offset + randomZ), Quaternion.Euler(0, rotations[Random.Range(0, rotations.Length)], 0), buildingParent);
                        //Instantiate(GetRandomRoofPrefab(), new Vector3(x * offset + randomX, heightOffset, z * offset + randomZ), Quaternion.Euler(0, rotations[Random.Range(0, rotations.Length)], 0), buildingParent);
                    }
                }
            }
        }

        private GameObject GetRandomBuildingPrefab()
        {
            return buildingPrefabs[Random.Range(0, buildingPrefabs.Count)];
        }

        // private GameObject GetRandomRoofPrefab()
        // {
        //     return roofPrefabs[Random.Range(0, roofPrefabs.Count)];
        // }
    }
}