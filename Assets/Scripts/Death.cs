using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public static void AnimDeath(GameObject gameObject, Vector3 pos, Quaternion rotation)
    {
        GameObject animDeath = null;
        Material mat;
        mat = gameObject.GetComponentInChildren<Renderer>().material;
        if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Bear")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_1_Death");
        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Bunny")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_2_Death");
        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Penguin")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_3_Death");
        else if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name == "Fox")
            animDeath = (GameObject)Resources.Load("Prefabs/Tipo_4_Death");

        if (gameObject.tag.Equals("Killer Guards"))
            animDeath = (GameObject)Resources.Load("Prefabs/Killer_Death");

        GameObject prefab = (GameObject)Instantiate(animDeath, pos, rotation);
        //prefab.transform.parent = gameObject.transform.parent;
        prefab.GetComponentInChildren<Renderer>().material = mat;
    }
}

