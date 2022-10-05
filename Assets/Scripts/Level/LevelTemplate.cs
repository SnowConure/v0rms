using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelTemplate", order = 1)]

public class LevelTemplate : ScriptableObject
{
    public string SceneName;

    public Texture2D PreviewImage;
}
