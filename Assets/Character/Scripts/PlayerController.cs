using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{
    public static bool isMoveable = true;
    private Animator animator;
    private Transform cameraTransform; 
    private float moveSpeed = 5f;
    private float rotationSpeed = 5f;
    private ServerManager serverManager;

    [SerializeField] private Text nicknameText; //머리위에 뜨는 text
    [SerializeField] private RuntimeAnimatorController thiefAnimationController; 
    [SerializeField] private RuntimeAnimatorController guardAnimationController;
    [SerializeField] private Sprite thiefSprite;
    [SerializeField] private Sprite guardSprite;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = PlayerSettings.userType == EPlayerType.Thief ? thiefAnimationController : guardAnimationController;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = PlayerSettings.userType == EPlayerType.Thief ? thiefSprite : guardSprite;
    }
    void Start()
    {
        nicknameText.text = PlayerSettings.userName;
        cameraTransform = Camera.main.transform;
        cameraTransform.localPosition = new Vector3(0f, 0f, -10f);
        serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        PlayerManager playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerManager.startGame();
    }

    private void Update()
    {
        if (isMoveable)
            Move();
    }

    private class PlayerData{
        public float[] position;
        public float[] rotation;
        public string userName;
        public bool isMove;
        public int userRoomIdx;
    }

    public void sendUserMoveInfo(bool isMove){
        PlayerData playerData = new PlayerData();
        Vector3 position = transform.position;
        playerData.position = new float[3]{
            position.x,
            position.y,
            position.z
        };
        Vector3 rotation = transform.rotation.eulerAngles;
        playerData.rotation = new float[3]{
            rotation.x,
            rotation.y,
            rotation.z
        };
        playerData.userName = PlayerSettings.userName;
        playerData.isMove = isMove;
        playerData.userRoomIdx = serverManager.getUserRoomIdx();
        string json = JsonUtility.ToJson(playerData);
        Debug.Log("USER MOVE: " + json);
        serverManager.emitMessage2("game/user-movement", json);
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
        
        sendUserMoveInfo(isMove);
        animator.SetBool("isMove", isMove);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="PWDocument"){
             //open popup that shows password
        }
    }
}
