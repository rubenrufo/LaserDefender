using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{

    [SerializeField] float lifeBarProportion = 1f;
    Player player;
    Image lifeBarImage;
    float lifeBarImageHeight;
    float lifeBarImageWidth;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        lifeBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeBarImageHeight = lifeBarImage.rectTransform.sizeDelta.y;
        lifeBarImageWidth = lifeBarProportion * player.GetPlayerHealth();
        lifeBarImage.rectTransform.sizeDelta = new Vector2(lifeBarImageWidth, lifeBarImageHeight);
    }


}
