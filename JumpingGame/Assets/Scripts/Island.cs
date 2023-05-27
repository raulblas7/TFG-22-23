
using UnityEngine;

public class Island : MonoBehaviour
{

    [SerializeField] private GameObject jumpDest;

    public GameObject GetJumpDest()
    {
        return jumpDest;
    }
}
