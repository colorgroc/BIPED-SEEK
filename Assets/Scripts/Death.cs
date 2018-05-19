using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public static void AnimDeath(GameObject gameObject, Vector3 pos, Quaternion rotation)
    {
        GameObject animDeath = null;
        Material mat = null;
        Material[] mats = null;

        if (!Tutorial_InGame.showIt)
            mat = gameObject.GetComponentInChildren<Renderer>().material;
        else mats = gameObject.GetComponentInChildren<Renderer>().materials;

        if (!gameObject.tag.Equals("Killer Guards"))
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
        else animDeath = (GameObject)Resources.Load("Prefabs/Killer_Death");

        GameObject prefab = (GameObject)Instantiate(animDeath, pos, rotation);
        if (!Tutorial_InGame.showIt)
        {
            if (!gameObject.tag.Equals("Killer Guards"))
                prefab.GetComponentInChildren<Renderer>().material = mat;
        }
        else prefab.GetComponentInChildren<Renderer>().materials = mats;
    }
}

