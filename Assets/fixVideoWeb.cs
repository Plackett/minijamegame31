using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixVideoWeb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UnityEngine.Video.VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "lynxvid.mp4");
    }
}
