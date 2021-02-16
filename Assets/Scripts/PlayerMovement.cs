using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour //skrypt odpowiedzialny na poruszanie się postaci jak i spadanie
{

    public CharacterController controller;
    public Transform isGround;
    public LayerMask groundMask;
    public float speed = 15.0f;
    public float gravity = -9.81f;
    public float groundDistance = 0.2f;
    public float jumpHeight = 3.0f;
    private float mapHeight=50f;
    private float mapWidth=50f;

    Vector3 velocity;
    bool isGrounded;

   
    void Update()
    {

        isGrounded = Physics.CheckSphere(isGround.position, groundDistance, groundMask);
        //obiekt gry reprezentujący gracza posiada dziecko "IsGround". Jest on nieco niżej niż kamera. On niego sprawdzane jest czy gracz dotyka podłoża
        if(isGrounded && velocity.y < 0){ //wyłaczanie graawitacji, jeżeli gracz jest na podłożu
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);//ruch po osiach X i Z
        
        if(!CheckIfPlayerIsOnTerrain())controller.Move(-move * speed * Time.deltaTime);//jezeli gracz miałby przekroczyć obszar terrainu, jego ruch zostaje cofnięty

        velocity.y += 0.5f * gravity * Time.deltaTime;//obliczanie prędkości "spadania".

        if (Input.GetButtonDown("Jump") && isGrounded)//skok jest wykonywany wtedy gdy postać stoi na podłożu i gracz wcisnął spacje
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);//obliczanie prędkości spadania po wyskoku
        }

        controller.Move(velocity * Time.deltaTime);//poruszanie po osi Y
       
    }

    bool CheckIfPlayerIsOnTerrain()//sprawdzane jest, czy gracz znajduje się wciąż na terrainie
    {
        bool suitableHeight=transform.position.x<mapHeight && transform.position.x>0;
        bool suitableWidth=transform.position.z<mapWidth && transform.position.z>0;
        return suitableHeight&&suitableWidth;
    }
}
