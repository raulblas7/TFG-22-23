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
       // Debug.Log("Lista fish count es " + fishList.Count);
        while(fishList.Count < numMaxOfFishInGame)
        {
            InstantiateFish();
        }
    }

    private void InstantiateFish()
    {
        int randomNum = Random.Range(0, spawners.Length);
        Fish fishGO = Instantiate(fishPrefab, spawners[randomNum].position, Quaternion.identity, transform);
        fishGO.SetFishingRod(fishingRod);
        fishGO.SetFishInstantiator(this);

        randomNum = Random.Range(0, fishSkins.materials.Length);
        fishGO.SetMaterialToMeshRenderer(fishSkins.materials[randomNum]);
        fishGO.SetPoints(fishSkins.points[randomNum]);

        randomNum = Random.Range(10, 20);
        fishGO.SetLifeTime(randomNum);

        fishList.Add(fishGO);
    }

    public void DeleteFishFromList(Fish fishToDelete)
    {
        fishList.Remove(fishToDelete);
        Destroy(fishToDelete.gameObject);
    }
}
