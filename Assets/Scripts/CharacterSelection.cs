using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
 


public class CharacterSelection : MonoBehaviour {
    

    // Use this for initialization
    void Start() {
        EventSystem.current.SetSelectedGameObject(null);
       // EventSystem.current.SetSelectedGameObject(this.gameObject);


    }

    // Update is called once per frame
    void Update() {
        
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Chosed() //que tipo de personaje elige
    {   
        List<GameObject> character = new List<GameObject>();
        foreach (Transform child in this.transform.parent)
        {
            character.Add(child.gameObject);
        }
        for (int i = 0; i < character.Count; i++)
        {
            if (this.gameObject != character[i])
            {
                character[i].GetComponent<Button>().interactable = false;
            }
        }
        for (int i = 1; i <= character.Count; i++) {

            if (this.gameObject.name.EndsWith(i.ToString()))
            {
                for (int j = 1; j <= NewControl.numOfPlayers; j++)
                {
                    if (this.transform.parent.name.EndsWith(j.ToString()))
                    {
                        PlayerPrefs.SetInt("characterPlayer_" + j.ToString(), i);
                    }
                }
            }

        }
        
    }  
    public void Click()
    {
        Debug.Log("Parent: " + this.transform.parent.gameObject.name);
        Debug.Log("Actual: " + this.gameObject.name);
    }
}



