using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Calculator : MonoBehaviour
{
    struct MusicInfo
    {
        public bool isImportant { get; }
        public int[] Nums { get; }
        public MusicInfo(bool x, int[] y)
        {
            isImportant = x;
            Nums = y;
        }
    }
    MusicInfo[] musics = 
    {
        //$ Junior
        new MusicInfo(false, new int[5]{00, 00, 21, 00, 21}),
        new MusicInfo(false, new int[5]{00, 21, 00, 21, 00}),
        new MusicInfo(false, new int[5]{21, 00, 00, 21, 00}),
        new MusicInfo(false, new int[5]{21, 00, 00, 21, 00}),
        new MusicInfo(false, new int[5]{00, 21, 00, 00, 21}),
        new MusicInfo(false, new int[5]{14, 00, 00, 16, 14}),
        new MusicInfo(false, new int[5]{00, 00, 32, 00, 12}),
        new MusicInfo(false, new int[5]{32, 00, 00, 21, 00}),   //8
        //$ Classic First Half
        new MusicInfo(true, new int[5]{00, 21, 00, 21, 00}),
        new MusicInfo(false, new int[5]{42, 00, 00, 21, 00}),
        new MusicInfo(false, new int[5]{21, 00, 00, 42, 00}),   //11
        //$ Classic Second Half
        new MusicInfo(true, new int[5]{21, 00, 21, 00, 21}),
        new MusicInfo(false, new int[5]{00, 42, 00, 00, 21}),
        new MusicInfo(false, new int[5]{00, 42, 21, 00, 00}),
        new MusicInfo(false, new int[5]{00, 00, 21, 00, 42}),   //15
        //$ Senior After
        new MusicInfo(false, new int[5]{00, 00, 22, 00, 22}),
        new MusicInfo(false, new int[5]{00, 22, 00, 00, 22}),
        new MusicInfo(false, new int[5]{12, 00, 00, 32, 00}),
        new MusicInfo(false, new int[5]{00, 32, 12, 00, 00}),
        new MusicInfo(true, new int[5]{42, 00, 00, 26, 00}),
        new MusicInfo(true, new int[5]{26, 00, 00, 42, 00}),    //21
    };
    [SerializeField] Toggle[] toggles;
    [SerializeField] TextMeshProUGUI[] tmps;
    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] SpriteRenderer[] blinders;
    [SerializeField] Button[] buttons;
    [SerializeField] TextMeshProUGUI TypeText;
    
    int Type = 0;
    int[] indexer = {8, 11, 15, 21};

    void Start()
    {
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        int[] ints = new int[5] { 0, 0, 0, 0, 0 };
        int[] importantInts = new int[5] { 0, 0, 0, 0, 0 };

        for (int i = 0; i < indexer[Type]; i++)
        {
            if (toggles[i].isOn)
            {
                sprites[i].enabled = true;
            }
            else
            {
                sprites[i].enabled = false;
                
                ints[0] += musics[i].Nums[0];
                ints[1] += musics[i].Nums[1];
                ints[2] += musics[i].Nums[2];
                ints[3] += musics[i].Nums[3];
                ints[4] += musics[i].Nums[4];

                if (musics[i].isImportant)
                {
                    importantInts[0] += musics[i].Nums[0];
                    importantInts[1] += musics[i].Nums[1];
                    importantInts[2] += musics[i].Nums[2];
                    importantInts[3] += musics[i].Nums[3];
                    importantInts[4] += musics[i].Nums[4];
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            tmps[i].text = ints[i] == 0 ? "--" : String.Format("{0:d2}", ints[i]);
            tmps[i + 5].text = importantInts[i] == 0 ? "--" : String.Format("{0:d2}", importantInts[i]);
        }
    }

    public void TypeChange(bool isUp)
    {
        if (isUp) { Type++; }
        else { Type--; }

        if (Type < 0) { Type = 0; }
        else if (Type > 3) { Type = 3; }

        if (Type == 0)
        {
            TypeText.text = "주니어";
            
            buttons[0].interactable = false;
            buttons[1].interactable = true;

            blinders[0].enabled = true;
            blinders[1].enabled = true;
            blinders[2].enabled = true;
            for (int i = 8; i < 21; i++)
            {
                toggles[i].isOn = false;
                toggles[i].interactable = false;
                sprites[i].enabled = false;
            }
        }
        else if (Type == 1)
        {
            TypeText.text = "클래식(전)";

            buttons[0].interactable = true;
            buttons[1].interactable = true;
            blinders[0].enabled = false;
            blinders[1].enabled = true;
            blinders[2].enabled = true;
            for (int i = 8; i < 11; i++)
            {
                toggles[i].interactable = true;
            }
            for (int i = 11; i < 21; i++)
            {
                toggles[i].isOn = false;
                toggles[i].interactable = false;
                sprites[i].enabled = false;
            }
        }
        else if (Type == 2)
        {
            TypeText.text = "클래식(후)";

            buttons[0].interactable = true;
            buttons[1].interactable = true;
            blinders[0].enabled = false;
            blinders[1].enabled = false;
            blinders[2].enabled = true;
            for (int i = 8; i < 15; i++)
            {
                toggles[i].interactable = true;
            }
            for (int i = 15; i < 21; i++)
            {
                toggles[i].isOn = false;
                toggles[i].interactable = false;
                sprites[i].enabled = false;
            }
        }
        else
        {
            TypeText.text = "시니어";

            buttons[0].interactable = true;
            buttons[1].interactable = false;
            blinders[0].enabled = false;
            blinders[1].enabled = false;
            blinders[2].enabled = false;
            for (int i = 8; i < 21; i++)
            {
                toggles[i].interactable = true;
            }
        }
    
        UpdateInfo();
    }

    public void Reset()
    {
        for (int i = 0; i < 21; i++)
        {
            toggles[i].isOn = false;
        }
        UpdateInfo();
    }
    public void ResetAll()
    {
        Type = 0;
        TypeChange(false);
        
        for (int i = 0; i < 21; i++)
        {
            toggles[i].isOn = false;
        }
        UpdateInfo();
    }
}
