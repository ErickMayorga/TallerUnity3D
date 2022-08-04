using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public Camera camara;
    public float speed = 1;
    public float runSpeed = 8;
    private Rigidbody cuerpo;
    public Animator animationController;
    public float jumpSpeed = 10f;
    private Vector3 direccion;
    public bool canJump = true;
    private Vector3 frenteCamara;
    private Vector3 derechaCamara;
    
    // Start is called before the first frame update
    void Start()
    {
        cuerpo = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        frenteCamara = camara.transform.forward;
        derechaCamara = camara.transform.right;
        frenteCamara.y = 0;
        derechaCamara.y = 0;
        frenteCamara = frenteCamara.normalized;
        derechaCamara = derechaCamara.normalized;


        direccion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direccion = direccion.x * derechaCamara + direccion.z * frenteCamara;
        transform.LookAt(transform.position + direccion);
        jumpCode();
        if(canJump){
            if(direccion == new Vector3(0,0,0)){
                animationController.SetBool("walk", false);    
            }
            else{
                animationController.SetBool("walk", true);
            }

            if(Input.GetButtonDown("Fire1")){
                animationController.SetTrigger("kick");
            }

            if(Input.GetButton("Fire3")){
                animationController.SetBool("run", true);
            }else{
                animationController.SetBool("run", false);
            }
        }
        // Movimiento por coordenadas Transform
        //this.transform.Translate(direccion * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Movimiento por impulso
        //cuerpo.AddForce(direccion * speed * Time.deltaTime);
        //cuerpo.MovePosition(transform.position + (direccion * speed * Time.deltaTime));
        
        if(Input.GetButton("Fire3")){
            cuerpo.MovePosition(transform.position + (direccion * runSpeed * Time.deltaTime));
        }else{
            cuerpo.MovePosition(transform.position + (direccion * speed * Time.deltaTime));
        }
    }

    private void jumpCode(){
        if(canJump){
            if(Input.GetButtonDown("Jump")){
                animationController.SetBool("jump", true);
                cuerpo.AddForce(new Vector3(0,jumpSpeed,0), ForceMode.Impulse);
            }
            animationController.SetBool("fall", false);
        }else{
            animationController.SetBool("jump", false);
            animationController.SetBool("fall", true);
        }
    }
}
