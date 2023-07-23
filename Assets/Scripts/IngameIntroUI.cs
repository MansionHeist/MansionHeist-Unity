using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameIntroUI : MonoBehaviour
{
    [SerializeField]
    private Text playerTypeText;
    [SerializeField]
    private GameObject intro;

    // Reference to the CharacterMover instance
    public CharacterMover characterMover;

    public void ShowPlayerType(){
        if(characterMover.playerType == EPlayerType.Guard){
            playerTypeText.text = "You Are Guard";
        }else{
            playerTypeText.text = "You Are Thief";
        }
    }

    public IEnumerator ShowIntroSequence(){
        intro.SetActive(true);
        ShowPlayerType();
        yield return new WaitForSeconds(3f);
        intro.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowIntroSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
