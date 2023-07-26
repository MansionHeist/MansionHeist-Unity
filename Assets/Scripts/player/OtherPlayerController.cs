using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class OtherPlayerController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Text nicknameText; //머리위에 뜨는 text
    [SerializeField] private RuntimeAnimatorController thiefAnimationController; 
    [SerializeField] private RuntimeAnimatorController guardAnimationController;
    [SerializeField] private Sprite thiefSprite;
    [SerializeField] private Sprite guardSprite;
    private string userName;

    public void setUserName(string _userName){
        userName = _userName;
        nicknameText.text = _userName;
    }

    public string getUserName(){
        return userName;
    }

    public void setUserType(string userType){
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = userType == "theif" ? thiefAnimationController : guardAnimationController;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = userType == "theif" ? thiefSprite : guardSprite;
    }

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    private void Update()
    {
        nicknameText.transform.rotation = Quaternion.identity;
        nicknameText.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, 0f);
    }

    public void Caught()
    {
        GameObject jail = transform.Find("Canvas").gameObject.transform.Find("Jail").gameObject;
        jail.SetActive(true);
        jail.transform.rotation = Quaternion.identity;
        //PlayerController.StopMoving();
        gameObject.tag = "CaughtPlayer";
    }

    public void GetOutOfJail()
    {
        // TODO: ArrestNearbyTheif()처럼, 체포/탈출에 사용되는 기존 player보다 좀더 넓은 collider를 설정하고, 충돌시 체포/타출땡이 가능하도록 하는 방법을 생각중..
        GameObject jail = transform.Find("Canvas").gameObject.transform.Find("Jail").gameObject;
        jail.SetActive(false);
        jail.transform.rotation = Quaternion.identity; 
        //PlayerController.isMoveable = true;
        gameObject.tag = "TheifPlayer";
    }

    /*private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="PWDocument"){
             //open popup that shows password
        }
    }*/
}
