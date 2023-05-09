using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInstantiator : MonoBehaviour
{
    [SerializeField] private Fish1 fishPrefab;
    [SerializeField] private FishingRod fishingRod;
    [SerializeField] private int numMaxOfFishInGame = 5;

    [SerializeField] private Transform[] spawners;
    [SerializeField] private Skin fishSkins;
    [SerializeField] private Transform deadZone;


    private List<Fish1> fishList;


    void Start()
    {
        GameManager.Instance.SetInstatiator(this);
        fishList = new List<Fish1>();
        //for (int i = 0; i < numMaxOfFishInGame; i++)
        //{
        //    InstantiateFish();
        //}
    }

    public void StartInstantiate()
    {
        for (int i = 0; i < numMaxOfFishInGame; i++)
        {
            InstantiateFish();
        }
    }


    void Update()
    {
        // Debug.Log("Lista fish count es " + fishList.Count);
        while (GameManager.Instance.IsGameActive() && fishList.Count < numMaxOfFishInGame)
        {
            InstantiateFish();
        }


    }

    private void InstantiateFish()
    {
        int randomNum = Random.Range(0, spawners.Length);
        Fish1 fishGO = Instantiate(fishPrefab, spawners[randomNum].position, Quaternion.identity, transform);
        fishGO.SetFishingRod(fishingRod);

        //pasamos color pez y puntos
        randomNum = Random.Range(0, fishSkins.materials.Length);
        fishGO.SetMaterialToMeshRenderer(fishSkins.materials[randomNum]);
        fishGO.SetPoints(fishSkins.points[randomNum]);

        //pasamos tiempo de vida
        randomNum = Random.Range(5, 12);
        fishGO.SetLifeTime(randomNum);

        //pasamos deadZone
        fishGO.SetDeadZoneTr(deadZone);

        //pasamos lista de objetivos
        fishGO.SetObjetivesList(spawners);

        //le decimos que elija primer destino
        fishGO.PickNewObjetive();

        fishList.Add(fishGO);
    }

    public void DeleteFishFromList(Fish1 fishToDelete)
    {
        fishList.Remove(fishToDelete);
        Destroy(fishToDelete.gameObject);
    }

    public void DeleteAllFish()
    {
        foreach (Fish1 f in fishList)
        {
            Destroy(f.gameObject);
        }
        fishList.Clear();
    }
}
