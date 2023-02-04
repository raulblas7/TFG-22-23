using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInstantiator : MonoBehaviour
{
    [SerializeField] private Fish fishPrefab;
    [SerializeField] private FishingRod fishingRod;
    [SerializeField] private int numMaxOfFishInGame = 5;

    [SerializeField] private Transform[] spawners;
    [SerializeField] private Skin fishSkins;

    private List<Fish> fishList;

    void Start()
    {
        fishList = new List<Fish>();
        for(int i = 0; i < numMaxOfFishInGame; i++)
        {
            InstantiateFish();
        }
    }

    void Update()
    {
        Debug.Log("Lista fish count es " + fishList.Count);
        while(fishList.Count < numMaxOfFishInGame)
        {
            InstantiateFish();
        }
    }

    private void InstantiateFish()
    {
        int randomSpawner = Random.Range(0, spawners.Length);
        Fish fishGO = Instantiate(fishPrefab, spawners[randomSpawner].position, Quaternion.identity, transform);
        fishGO.SetFishingRod(fishingRod);
        fishGO.SetFishInstantiator(this);

        int randomSkin = Random.Range(0, fishSkins.materials.Length);
        fishGO.SetMaterialToMeshRenderer(fishSkins.materials[randomSkin]);

        fishList.Add(fishGO);
    }

    public void DeleteFishFromList(Fish fishToDelete)
    {
        fishList.Remove(fishToDelete);
        Destroy(fishToDelete.gameObject);
    }
}
