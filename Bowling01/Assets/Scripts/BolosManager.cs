using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BolosManager : MonoBehaviour
{

    [SerializeField] Bolo prefabBolo;
    [SerializeField] Transform[] positions;

    private List<Bolo> bolos;
    private int maxNumBolos;
    private int firstPartPoints = 0; // variable auxiliar para distinguir los
                                     // puntos de la primera parte de la ronda
                                     // de los puntos de la segunda parte


    void Start()
    {
        maxNumBolos = positions.Length;
        bolos = new List<Bolo>();
        GameManager.Instance.SetBolosManager(this);
        instatiateBolos();

    }



    public void deleteBoloFromList(Bolo b)
    {
        
       if(bolos.Remove(b))
        {
           // Debug.Log("Eliminado bolo " + b._index);
        }
    }

    public void instatiateBolos()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 aux = positions[i].position;
            Vector3 pos = new Vector3(aux.x, aux.y + 3, aux.z);
            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(-90, 0, 0);
            Bolo b = Instantiate<Bolo>(prefabBolo, pos, rot, transform);
            b.SetInfo(this, i);
            bolos.Add(b);
        }
    }

    public void LetBolosFall()
    {
        for (int i = 0; i < bolos.Count; i++)
        {
            bolos[i].Fall();
        }
    }
    public void CheckOnthefloor()
    {
        //Debug.Log(bolos.Count);
        for (int i = 0; i < bolos.Count; i++)
        {
            if (!bolos[i].IsOnTheFloor())
            {
                bolos[i].ElevateBolo();
            }
        }
    }

    // se encarga de comprobar los puntos
    public int CheckPoints(bool firstPart )
    {
        int standUpBowling = 0;
       
        for (int i = 0; i < bolos.Count; i++)
        {
            if (!bolos[i].IsOnTheFloor())
            {
                standUpBowling++;
             
            }
        }
        if (firstPart)
        {
            firstPartPoints = maxNumBolos - standUpBowling;
        }

        Debug.Log(standUpBowling);

        Debug.Log(firstPartPoints);

        // return maxNumBolos - standUpBowling;
        return firstPart ? maxNumBolos - standUpBowling : maxNumBolos - standUpBowling - firstPartPoints;
    }
}
