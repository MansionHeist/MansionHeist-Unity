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

    public void setUserName(string userName){
        nicknameText.text = userName;
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

    

    /*private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="PWDocument"){
             //open popup that shows password
        }
    }*/
}
