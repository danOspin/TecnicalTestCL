using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{

    public Sprite[] sprites_boy;
    public Sprite[] sprites_girl;


    public Sprite RandomSprite()
    {
        int number = UnityEngine.Random.Range(0, 99);
        if (number >= 50)
        {
            return sprites_girl[0];
        }
        else
            return sprites_boy[0];
    }
}
