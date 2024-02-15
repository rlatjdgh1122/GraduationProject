using System;
using UnityEngine;

public abstract class NormalUI : MonoBehaviour
{
    public UIType UIType;

    public virtual void Awake()
    {

    }

    public abstract void EnableUI(float time, object obj); //UI�� ��� �� ����ϴ� �Լ�. DOFade�� ������ Ȱ��ȭ �ϰ� time���� ���ʵ��� Ȱ��ȭ�Ұ��� �����ش�.
    
    public abstract void DisableUI(float time, Action action); //UI�� �� �� ����ϴ� �Լ�. DOFade�� ������ ��Ȱ��ȭ �ϰ� ���������� time���� ��Ȱ��ȭ �ð� �����ִµ�, Action�� �Ű������� �Ѱ� ��Ȱ��ȭ ���Ŀ� ������ �Ұ������� ������ �� �ִ�.
}
