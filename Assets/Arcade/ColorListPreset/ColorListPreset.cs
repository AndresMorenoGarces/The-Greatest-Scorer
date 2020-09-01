using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorListPreset", menuName = "Assets/Arcade/ColorListPreset")]
public class ColorListPreset : ScriptableObject{
    public Color[] backGroundColorList;
    public Color[] spikeColorList; 
    public Gradient[] particlesColorList; 
}
