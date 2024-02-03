using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;
    public TileManager tileManager;

    private void Awake() {
        if (singleton != null && singleton != this) { // there can only be one
            Destroy(this.gameObject);
        } else {
            singleton = this;
        }

        DontDestroyOnLoad(this.gameObject);
        tileManager = GetComponent<TileManager>();
    }
}
