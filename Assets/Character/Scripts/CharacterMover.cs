using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EPlayerType{
    Guard,
    Thief
}

public class CharacterMover : MonoBehaviour
{
    public EPlayerType playerType = EPlayerType.Guard;

    private Animator animator;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform cameraTransform;
    private Vector2 size = new Vector2(1f,1f);

    [SerializeField] private Text nicknameText;

    public void SetNickname(string value){
         nicknameText.text = value;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="PWDocument"){
             //open popup that shows password
        }
    }

    void Awake()
    {
        //SetNickname(PlayerSettings.userName);        
        SetNickname("Guard");
        animator = GetComponent<Animator>();
        // Get reference to the camera transform
        cameraTransform = Camera.main.transform;

        // Set the camera's initial position and size
        cameraTransform.localPosition = new Vector3(0f, 0f, -10f);

    }

    private void Start(){
    // Find the GameSystem object in the scene and get its GameSystem component
    GameSystem gameSystem = FindObjectOfType<GameSystem>();
    
    // Check if the GameSystem object was found
    if (gameSystem != null)
    {
        // Call the AddPlayer method on the GameSystem instance
        gameSystem.AddPlayer(this);
    }
    else
    {
        Debug.LogError("GameSystem object not found in the scene!");
    }
}


    private void Update()
    {
        Move();
    
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
        animator.SetBool("isMove",isMove);
    }
}
