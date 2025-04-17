using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_GrassBiomePrefabs;
    [SerializeField] private GameObject[] m_SnowBiomePrefabs;
    [SerializeField] private GameObject[] m_IceBiomePrefabs;

    [SerializeField] private GameObject m_StartPlatform;
    [SerializeField] private int m_MaxPlatformCount;
    [SerializeField] private float m_SpawnDistance;
    [SerializeField] private int BiomeChangeDistance;
    [SerializeField] GameObject m_Player;
    private GameObject m_MostRecentPlatformLoaded;
    private Queue<GameObject> m_PlatformsQueue;
    private int m_Biome = 0; // 0 = grass, 1 = lava, 2 = ice

    void Start()
    {
        m_PlatformsQueue = new Queue<GameObject>();
        m_MostRecentPlatformLoaded = m_StartPlatform;
        m_PlatformsQueue.Enqueue(m_MostRecentPlatformLoaded);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBiome();
        if (Vector3.Distance(m_Player.transform.position, m_MostRecentPlatformLoaded.transform.position) < m_SpawnDistance)
        {
            Transform EndPoint = m_MostRecentPlatformLoaded.transform.Find("End");
            m_MostRecentPlatformLoaded = SpawnRandomPlatform(EndPoint.position);

            m_PlatformsQueue.Enqueue(m_MostRecentPlatformLoaded);
            if (m_PlatformsQueue.Count > m_MaxPlatformCount)
            {
                GameObject platformToDelete = m_PlatformsQueue.Dequeue();
                Destroy(platformToDelete.gameObject);
            }
        }
    }

    private GameObject returnRandomPlatformBasedOnBiome()
    {
        if (m_Biome == 0)
        {
            int RandomIndexChoice = Random.Range(0, m_GrassBiomePrefabs.Length);
            return m_GrassBiomePrefabs[RandomIndexChoice];
        } else if (m_Biome == 1)
        {
            int RandomIndexChoice = Random.Range(0, m_SnowBiomePrefabs.Length);
            return m_SnowBiomePrefabs[RandomIndexChoice];
        } else if (m_Biome == 2)
        {
            int RandomIndexChoice = Random.Range(0, m_IceBiomePrefabs.Length);
            return m_IceBiomePrefabs[RandomIndexChoice];
        }
        return m_GrassBiomePrefabs[1]; // Pour pas avoir d'erreur dans la fonction. cette ligne ne sera jamais atteinte
    }

    private void ChangeBiome()
    {
        m_Biome = ((int)m_Player.transform.position.x / BiomeChangeDistance) % 3;
    }

    private GameObject SpawnRandomPlatform(Vector3 SpawnPosition)
    {
        GameObject newPlatform = Instantiate(returnRandomPlatformBasedOnBiome(), SpawnPosition, Quaternion.identity);
        return newPlatform;
    }
}
