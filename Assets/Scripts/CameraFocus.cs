using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private SpriteRenderer focus;
    // Removes gaps between different screen sizes
    void Start()
    {
        focus = GetComponent<SpriteRenderer>();
        float ScreenHeight = Camera.main.orthographicSize * 2;
        float ScreenWidth = ScreenHeight / Screen.height * Screen.width;
        transform.localScale = new Vector3(ScreenWidth / focus.sprite.bounds.size.x, ScreenWidth / focus.sprite.bounds.size.y, 1);
    }
}
