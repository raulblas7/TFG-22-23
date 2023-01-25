using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolo : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float _upForce;

    private BolosManager _parent;
    public int _index;
    private bool _onTheFloor = false;

    void Start()
    {
        
    }

  
   
       
    

    public void SetInfo(BolosManager parent, int index)
    {
        _parent = parent;
        _index = index;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            _parent.deleteBoloFromList(this);
            Destroy(this.gameObject);
        }
    }

    public void OnThefloor()
    {
        _onTheFloor = true;
    }

    public bool IsOnTheFloor() { return _onTheFloor; }

    public void ElevateBolo()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.up * _upForce);
        Invoke("StopGoingUp", 3);
    }
    private void StopGoingUp()
    {
        rb.AddForce(Vector3.down * _upForce);
    }

    public void Fall()
    {
        rb.useGravity = true;
    }
}