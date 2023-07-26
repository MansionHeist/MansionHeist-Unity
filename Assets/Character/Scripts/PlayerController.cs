using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{
    public static bool isMoveable = true;
    private static Animator sAnimator;
    private Animator animator;
    private Transform cameraTransform; 
    private float moveSpeed = 5f;
    private float rotationSpeed = 5f;
    private ServerManager serverManager;
    GameObject closestTheif = null;

    [SerializeField] private Text nicknameText; //머리위에 뜨는 text
    [SerializeField] private RuntimeAnimatorController thiefAnimationController; 
    [SerializeField] private RuntimeAnimatorController guardAnimationController;
    [SerializeField] private Sprite thiefSprite;
    [SerializeField] private Sprite guardSprite;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = PlayerSettings.userType == EPlayerType.Thief ? thiefAnimationController : guardAnimationController;

        if(sAnimator == null)
        {
            sAnimator = animator;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = PlayerSettings.userType == EPlayerType.Thief ? thiefSprite : guardSprite;
    }
    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.localPosition = new Vector3(0f, 0f, -10f);
        serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        PlayerManager playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerManager.startGame();
    }

    private void OnEnable()
    {
        nicknameText.text = PlayerSettings.userName;
    }

    private void Update()
    {
        if (isMoveable){
            Move();
            if(PlayerSettings.userType == EPlayerType.Guard){
                checkArrestButton();
            }else{
                checkUnlockTheif();
            }
        }
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        nicknameText.transform.rotation = Quaternion.identity;
        nicknameText.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, 0f);

    }

    private void checkArrestButton(){
        GameObject arrestButton = GameObject.Find("GuardUI").transform.GetChild(0).gameObject;
        closestTheif = null;
        float closestDistance = 3.0f;
        foreach(GameObject theifObj in GameObject.FindGameObjectsWithTag("TheifPlayer")){
            Vector3 distance = theifObj.transform.position - transform.position;
            float curDistance = distance.sqrMagnitude;
            if(curDistance < closestDistance){
                closestDistance = curDistance;
                closestTheif = theifObj;
            }
        }
        if(closestTheif!=null){
            arrestButton.SetActive(true);
        }else{
            arrestButton.SetActive(false);
        }
    }
    
    private void checkUnlockTheif(){
        closestTheif = null;
        float closestDistance = 1.0f;
        foreach(GameObject theifObj in GameObject.FindGameObjectsWithTag("CaughtPlayer")){
            Vector3 distance = theifObj.transform.position - transform.position;
            float curDistance = distance.sqrMagnitude;
            if(curDistance < closestDistance){
                closestDistance = curDistance;
                closestTheif = theifObj;
            }
        }
        if(closestTheif!=null){
            OtherPlayerController otherPlayerController = closestTheif.GetComponent<OtherPlayerController>();
            string theifName = otherPlayerController.getUserName();
            serverManager.emitMessage("game/unlock-theif", theifName);
        }
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

        
        // Move the character in the direction of the input
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Keep the camera fixed in the background (no rotation or movement)
        bool isMove = moveDirection.magnitude!=0f;
        
        sendUserMoveInfo(isMove);
        animator.SetBool("isMove", isMove);
    }

    public static void StopMoving()
    {
        PlayerController.isMoveable = false;
        sAnimator.SetBool("isMove", false);
    }
    
    public void ArrestNearbyThief()
    {
        // TODO: thief에 대한 처리
        // 근처라는 기준을 어떻게 잡을 것인지?
        // collider로 처리할 건지, 아니면 distance를 기준으로 계산할건지
        Debug.Log("ArrestNearbyThief: " + closestTheif.name);
        OtherPlayerController otherPlayerController = closestTheif.GetComponent<OtherPlayerController>();
        string theifName = otherPlayerController.getUserName();
        
        serverManager.emitMessage("game/arrest-theif", theifName);
        //otherPlayerController.Caught();

    }

    public void Caught()
    {
        GameObject jail = transform.Find("Canvas").gameObject.transform.Find("Jail").gameObject;
        jail.SetActive(true);
        jail.transform.rotation = Quaternion.identity;
        PlayerController.StopMoving();
        gameObject.tag = "CaughtPlayer";
        GameObject missionUi = GameObject.Find("Map").transform.GetChild(1).gameObject;
        missionUi.SetActive(false);
    }

    public void GetOutOfJail()
    {
        // TODO: ArrestNearbyTheif()처럼, 체포/탈출에 사용되는 기존 player보다 좀더 넓은 collider를 설정하고, 충돌시 체포/타출땡이 가능하도록 하는 방법을 생각중..
        GameObject jail = transform.Find("Canvas").gameObject.transform.Find("Jail").gameObject;
        jail.SetActive(false);
        jail.transform.rotation = Quaternion.identity; 
        PlayerController.isMoveable = true;
        gameObject.tag = "TheifPlayer";
        GameObject missionUi = GameObject.Find("Map").transform.GetChild(1).gameObject;
        missionUi.SetActive(true);
    }
}