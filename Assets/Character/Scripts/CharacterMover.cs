using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMover : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform cameraTransform;
    private Vector2 size = new Vector2(1f,1f);

    // [SerializeField] private Text nicknameText;
    // public void SetNickname(string value){
    //     {nicknameText.text = value;}
    // }

    void Start()
    {
        //SetNickname(PlayerSettings.userName);        
        animator = GetComponent<Animator>();
        // Get reference to the camera transform
        cameraTransform = Camera.main.transform;

        // Set the camera's initial position and size
        cameraTransform.localPosition = new Vector3(0f, 0f, -10f);
    }

    private void Update()
    {
        Move();
        Collider2D hit = Physics2D.OverlapBox(transform.position, size, 0, 0);
        if (hit != null)
    {
        Debug.Log(hit.name);
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

        // Move the character in the direction of the input
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Keep the camera fixed in the background (no rotation or movement)
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        bool isMove = moveDirection.magnitude!=0f;
        animator.SetBool("isMove",isMove);
    }
}
