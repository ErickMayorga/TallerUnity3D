using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pies : MonoBehaviour
{
    // Start is called before the first frame update
    public Movimiento canJump;

    private void OnTriggerStay(Collider other){
        canJump.canJump = true;
    }

    private void OnTriggerExit(Collider other){
        canJump.canJump = false;
    }

    
}
