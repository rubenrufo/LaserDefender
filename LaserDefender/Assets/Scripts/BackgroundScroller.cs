using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float backgroundScrollSpeed = 0.2f;
    [SerializeField] float lateralShiftProp = 0.1f;
    [SerializeField] Player player;
    Material myMaterial;
    Vector2 offset;
    Vector2 totalOffset;
    Vector2 lastTexturePosition;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
        totalOffset = offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerPos = player.GetPlayerPosition();
            myMaterial.mainTextureOffset = new Vector2(-playerPos.x * 0.1f * lateralShiftProp, - (playerPos.y * 0.1f * lateralShiftProp));
            lastTexturePosition = myMaterial.mainTextureOffset;
        }
        else
        {
            myMaterial.mainTextureOffset = lastTexturePosition;
        }
        totalOffset += offset * Time.deltaTime;
        myMaterial.mainTextureOffset += totalOffset;
    }
}
