using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBolos : MonoBehaviour
{

    [SerializeField] GameObject prefabBolo;
    [SerializeField] Transform[] positions;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 aux = positions[i].position;
            Vector3 pos = new Vector3(aux.x, aux.y + 3, aux.z);
            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(-90,0,0);
            Instantiate(prefabBolo, pos,rot, transform);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
