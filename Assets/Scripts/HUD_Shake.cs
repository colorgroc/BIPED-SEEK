using UnityEngine;
using System.Collections;

public class HUD_Shake : MonoBehaviour
{
    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private GameObject player;
   // private float _fakeDelta;
    private void Start()
    {
        this._originalPos = this.gameObject.transform.localPosition;
        if (this.gameObject.name.EndsWith("1")) this.player = GameObject.Find("Player 1");
        else if (this.gameObject.name.EndsWith("2")) this.player = GameObject.Find("Player 2");
        else if (this.gameObject.name.EndsWith("3")) this.player = GameObject.Find("Player 3");
        else if (this.gameObject.name.EndsWith("4")) this.player = GameObject.Find("Player 4");
    }
    private void Update()
    {
        Shake(this.player.GetComponent<PlayerControl>().detected, 5f);
    }
    public void Shake(bool duration, float amount)
    { 
        StopAllCoroutines();
        StartCoroutine(this.cShake(duration, amount));
    }

    public IEnumerator cShake(bool duration, float amount)
    {

        while (duration)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}

