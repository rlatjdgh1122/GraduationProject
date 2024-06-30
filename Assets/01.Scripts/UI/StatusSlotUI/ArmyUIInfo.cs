using UnityEngine;

public class ArmyUIInfo
{
    public Sprite SynergySprite = null; //ó���� ���翡 ���� �������� �屺�� ������ �ٲ�
    public Sprite SkillSprite = null; //�屺 �ٲ� �ٲ�
    public Sprite UltimateSprite = null; //ó���� �������� �ȹٲ�
    public int PenguinCount = 0; //���� ���� ���� ���� �޶���
    public string ArmyName = string.Empty; //�̸� �ٲ𶧸��� �޶���   

    public OnValueUpdated<ArmyUIInfo> OnValueUpdated = null;
    public void AddCount(int value = 1)
    {
        PenguinCount += value;
    }

    public void RemoveCount(int value = 1)
    {
        PenguinCount -= value;
    }


}
