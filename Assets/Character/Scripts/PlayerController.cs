using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EPlayerType{
    Guard,
    Thief
}

public class PlayerController : MonoBehaviour
{
    public EPlayerType playerType = EPlayerType.Thief;
    private Animator animator;
    private Transform cameraTransform;
    
    [SerializeField] private Text nicknameText; //머리위에 뜨는 text

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    

    public void SetNickname(string value){
         nicknameText.text = value;
    }

    void Awake()
    {
        SetNickname("Thief");
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        cameraTransform.localPosition = new Vector3(0f, 0f, -10f);

    }

    private void Update()
    {
        if(isCurrentPlayer){
            Move();
        }
    }
    

    public void Move(){
        // Read keyboard input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on input values
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // If there is any input, rotate the character towards the direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        nicknameText.transform.rotation = Quaternion.identity;
        nicknameText.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, 0f);

        // Move the character in the direction of the input
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Keep the camera fixed in the background (no rotation or movement)
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        bool isMove = moveDirection.magnitude!=0f;
        //animator.SetBool("isMove",isMove);
    }

    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="PWDocument"){
             //open popup that shows password
        }
    }


}
