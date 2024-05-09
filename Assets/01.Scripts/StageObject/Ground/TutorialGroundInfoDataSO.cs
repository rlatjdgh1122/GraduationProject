using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TutorialGroundInfoData")]
public class TutorialGroundInfoDataSO : ScriptableObject
{
    public List<ComingEnemiesInfo> TutorialComingEnemies = new List<ComingEnemiesInfo>();

    public List<ResourceGeneratePattern> TutorialResourceGeneratePattern = new List<ResourceGeneratePattern>();
}
