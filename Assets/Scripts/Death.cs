using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public static void AnimDeath(GameObject gameObject, Vector3 pos, Quaternion rotation)
    {
        GameObject animDeath = null;
        Material mat;
        Material[] mats;
        if (!Tutorial_InGame.showIt && !gameObject.tag.Equals("Killer Guards"))
            mat = gameObject.GetComponentInChildren<Renderer>().material;
        else mat = gameObject.GetComponentInChildren<Renderer>().materials[0];
        if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Bear")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_1_Death");
        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Bunny")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_2_Death");
        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Penguin")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_3_Death");
        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Fox")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_4_Death");

        if (gameObject.tag.Equals("Killer Guards"))
        {
            animDeath = (GameObject)Resources.Load("Prefabs/Killer_Death");
        }

        GameObject prefab = (GameObject)Instantiate(animDeath, pos, rotation);
        //prefab.transform.parent = gameObject.transform.parent;
        if(gameObject.tag.Equals("Killer Guards"))
        {
            mats = gameObject.GetComponentInChildren<Renderer>().materials;
            prefab.GetComponentInChildren<Renderer>().materials = mats;
        }else prefab.GetComponentInChildren<Renderer>().material = mat;

    }
}

