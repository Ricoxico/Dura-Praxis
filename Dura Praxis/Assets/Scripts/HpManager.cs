using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public static HpManager instance;

    public Text hpText;
    int hp = 100;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpText.text = "HP: " + hp.ToString();
    }

    public void ChangeHealth(int healthchange)
    {
        hp += healthchange;
        if (hp >= 100)
            hp = 100;
        hpText.text = "HP: " + hp.ToString();
    }
}
