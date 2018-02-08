using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/* JoystickButton0 - A
 JoystickButton1 - B
 JoystickButton2 - X
 JoystickButton3 - Y
 JoystickButton4  - LB
 JoystickButton5  - RB
 JoystickButton6  - LT
 JoystickButton7  - RT
 JoystickButton8 - back
 JoystickButton9 - start
 JoystickButton10 - left stick[not direction, button]
 JoystickButton11 - right stick[not direction, button]*/


public class CharacterSelection : MonoBehaviour {
    private GameObject[] jugadores;
    System.Array values;
    List<GameObject> characterTypes;
    //GameObject but;
    // Use this for initialization
    void Start() {
        this.characterTypes = new List<GameObject>();
        foreach (Transform child in this.transform.parent)
        {
            this.characterTypes.Add(child.gameObject);
        }

        jugadores = GameObject.FindGameObjectsWithTag("Seleccion Personajes");

        EventSystem.current.SetSelectedGameObject(null);
        if (this.gameObject.name.EndsWith("1"))
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            //this.but = this.gameObject.GetComponent<Button>().gameObject;
        }
            //EventSystem.current.SetSelectedGameObject(this.but);

        values = System.Enum.GetValues(typeof(KeyCode));
    }

    // Update is called once per frame
    void Update() {
        //for (int j = 0; j < PlayerPrefs.GetInt("NumPlayers"); j++)
        //{
           // Button buton;
            if (Input.GetButtonDown("B"))
            {
                for (int i = 0; i < this.characterTypes.Count; i++)
                {
                    this.characterTypes[i].GetComponent<Button>().interactable = true;
                }
                //characterTypes[j].GetComponent<Button>().onClick = null;
                

                Debug.Log("B");
               // buton.onClick = null;
            }
         
            /*if (this.transform.parent.name.EndsWith(j.ToString()))
            {
                PlayerPrefs.SetInt("characterPlayer_" + j.ToString(), null);
            }*/

        //}
        /* foreach (KeyCode code in values)
         {
             if (Input.GetKeyUp(code)) {
                 // print(System.Enum.GetName(typeof(KeyCode), code));
                 Debug.Log(code.ToString());
                 pressed = true;
             }
         }*/




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
                for (int j = 1; j <= PlayerPrefs.GetInt("NumPlayers"); j++)
                {
                    if (this.transform.parent.name.EndsWith(j.ToString()))
                    {
                        PlayerPrefs.SetInt("characterPlayer_" + j.ToString(), i);
                    }
                }
            }
        }
    }
    List<GameObject> GetButtons()
    {
        List<GameObject> character = new List<GameObject>();
        foreach (Transform child in this.transform.parent)
        {
            character.Add(child.gameObject);
        }
        return character;
    }
    public void Click()
        {
           // Debug.Log("Parent: " + this.transform.parent.gameObject.name);
           // Debug.Log("Actual: " + this.gameObject.name);
        }
    }



