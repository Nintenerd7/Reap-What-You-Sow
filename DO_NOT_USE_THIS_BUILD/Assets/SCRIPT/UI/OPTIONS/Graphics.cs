using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Graphics : MonoBehaviour
{
    public Toggle FullScreenTog, SyncTog;
    public List<Item> RES = new List<Item>();
    private int SelectedRes;
    public Text ResLable;
    // Start is called before the first frame update
    void Start()
    {
        FullScreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            SyncTog.isOn = false;
        }
        else
        {
            SyncTog.isOn = true;
        }

        bool ResTog = false;

        for (int i = 0; i < RES.Count; i++)
        {
            if (Screen.width == RES[i].Horizontal && Screen.height == RES[i].Vertical)
            {
                ResTog = true;
                SelectedRes = i;
                UpdateText();
            }
        }

        if (!ResTog)
        {
            Item NewRes = new Item();
            NewRes.Horizontal = Screen.width;
            NewRes.Vertical = Screen.height;

            RES.Add(NewRes);
            SelectedRes = RES.Count - 1;
            UpdateText();
        }

    }
    //RESOLUTION LOGIC
    public void LeftArrow()
    {
        SelectedRes--;
        if (SelectedRes < 0)
        {
            SelectedRes = 0;
        }
        UpdateText();
    }

    public void RightArrow()
    {
        SelectedRes++;
        if (SelectedRes > RES.Count - 1)
        {
            SelectedRes = RES.Count - 1;
        }
        UpdateText();
    }

    public void UpdateText()
    {
        ResLable.text = RES[SelectedRes].Horizontal.ToString() + "x" + RES[SelectedRes].Vertical.ToString();
    }
    //END OF RESOLUTION LOGIC
    public void ApplySettings()
    {

        if (SyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(RES[SelectedRes].Horizontal, RES[SelectedRes].Vertical, FullScreenTog.isOn);
    }
}
[System.Serializable]
public class Item
{
    public int Horizontal, Vertical;
}