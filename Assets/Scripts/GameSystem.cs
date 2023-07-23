using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    private List<CharacterMover> players = new List<CharacterMover>();

    public void AddPlayer(CharacterMover player){
        if(!players.Contains(player)){
            players.Add(player);
        }
    }
    
    public List<CharacterMover> GetPlayerList(){
        return players;
    }

    private void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
