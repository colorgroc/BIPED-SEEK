using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    public static void AnimDeath(GameObject gameObject, Vector3 pos, Quaternion rotation)
    {
        GameObject animDeath = null;
        Material mat = null;
        Material[] mats = null;
        GameObject prefab = null;

        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            if (gameObject != null && gameObject.GetComponentInChildren<Renderer>() != null && gameObject.GetComponentInChildren<Renderer>().material != null)
                mat = gameObject.GetComponentInChildren<Renderer>().material;
        }
        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (gameObject != null && gameObject.GetComponentInChildren<Renderer>() != null && gameObject.GetComponentInChildren<Renderer>().materials != null)
                mats = gameObject.GetComponentInChildren<Renderer>().materials;
        }
        if (gameObject != null )
        {
            if (!gameObject.tag.Equals("Killer Guards"))
                if(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject != null) {
                    {
                        if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Bear")
                        {
                            if (gameObject.tag == "Guard") animDeath = (GameObject)Resources.Load("Prefabs/Tipo_1_Death_NPC");
                            else animDeath = (GameObject)Resources.Load("Prefabs/Tipo_1_Death");
                        }
                        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Bunny")
                        {
                            if (gameObject.tag == "Guard") animDeath = (GameObject)Resources.Load("Prefabs/Tipo_2_Death_NPC");
                            else animDeath = (GameObject)Resources.Load("Prefabs/Tipo_2_Death");
                        }
                        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Penguin")
                        {
                            if (gameObject.tag == "Guard") animDeath = (GameObject)Resources.Load("Prefabs/Tipo_3_Death_NPC");
                            else animDeath = (GameObject)Resources.Load("Prefabs/Tipo_3_Death");
                        }
                        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Fox")
                        {
                            if (gameObject.tag == "Guard") animDeath = (GameObject)Resources.Load("Prefabs/Tipo_4_Death_NPC");
                            else animDeath = (GameObject)Resources.Load("Prefabs/Tipo_4_Death");
                        }
                    }
            }
            else animDeath = (GameObject)Resources.Load("Prefabs/Killer_Death");
            if (animDeath != null)
                prefab = (GameObject)Instantiate(animDeath, pos, rotation);

            if (!Tutorial_InGame.showIt && !Abilities_Tutorial.show)
            {
                if (!gameObject.tag.Equals("Killer Guards") && mat != null && prefab != null)
                    prefab.GetComponentInChildren<Renderer>().material = mat;
            }
            else
            {
                if (mats.Length > 0 && prefab != null) prefab.GetComponentInChildren<Renderer>().materials = mats;
            }
        }
    }
}

