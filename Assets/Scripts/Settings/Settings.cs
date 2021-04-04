using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle fullscreen;
    public Dropdown DropDownResolutions;
    public Slider volume;
    Resolution[] curResolutions;
    void Start()
    {
        Screen.fullScreen = true;
        fullscreen.isOn = true;
        Resolution[] resolutions = Screen.resolutions;
        curResolutions = resolutions.Distinct().ToArray();
        string[] strRes = new string[curResolutions.Length];
        for (int i=0;i<curResolutions.Length;i++)
        {
            strRes[i] = curResolutions[i].width.ToString()+"x"+curResolutions[i].height.ToString();
        }
        DropDownResolutions.ClearOptions();
        DropDownResolutions.AddOptions(strRes.ToList());
        Screen.SetResolution(curResolutions[curResolutions.Length - 1].width, curResolutions[curResolutions.Length - 1].height, fullscreen.isOn);
    } 

    public void ScreenMode()
    {
        Screen.fullScreen = fullscreen.isOn;
    }
    public void SetCurResolution()
    {
        Screen.SetResolution(curResolutions[DropDownResolutions.value].width, curResolutions[DropDownResolutions.value].height, fullscreen.isOn);
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volume.value;
    }
}
