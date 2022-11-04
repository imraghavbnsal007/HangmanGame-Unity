using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public Text WordToFind;
    private float time;
    private string chosenWord;
    private string hiddenWord;
    public GameObject[] hangman;
    private int fails;
    public GameObject wintext;
    public GameObject losetext;
    private bool gameEnd = false;

    private string[] wordsLocal = {"RAGHAV BANSAL", "AYAN", "LOVISH", "KHUSHBOO", "AAYUSH"};
    // Start is called before the first frame update
    void Start()
    {   
        
        chosenWord = wordsLocal[Random.Range(0,wordsLocal.Length)];
        
        for(int i =0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if(char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "_";
                
            }
            
        }
        WordToFind.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd == false)
        {
            time += Time.deltaTime;
        timeField.text = time.ToString();


        }
        

        
    }

    private void OnGUI(){
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedkey = e.keyCode.ToString();
            Debug.Log("keydown event was triggered" + pressedkey );

            if (chosenWord.Contains(pressedkey))
            {
                // hiddenWord = _ _ _ _ _
                // chosenWord = R A G H A v
                //Set new hidden word to everything before the i,
                //Change the i to the letter presser, and everything after the i  
                int i = chosenWord.IndexOf(pressedkey);
                while(i != -1)
                {
                    hiddenWord = hiddenWord.Substring(0,i) + pressedkey + hiddenWord.Substring(i +1);
                    Debug.Log(hiddenWord);

                    chosenWord= chosenWord.Substring(0,i) + "_" + chosenWord.Substring(i+1);
                    Debug.Log(chosenWord);
                    i = chosenWord.IndexOf(pressedkey);
                }
                WordToFind.text = hiddenWord;


            }
            //add a hangman body part
            else
            {
                hangman[fails].SetActive(true);
                
                fails++;
            }
            if (fails == hangman.Length)
            {
                losetext.SetActive(true);
                gameEnd = true;
            }
            if(!hiddenWord.Contains("_"))
            {
                wintext.SetActive(true);
                gameEnd = true;
            }
        }
    }
}
