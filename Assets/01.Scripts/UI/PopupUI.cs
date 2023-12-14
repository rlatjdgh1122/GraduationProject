using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupUI : MonoBehaviour
{
    public UI UIType;

    public virtual void Awake()
    {

    }

    public abstract void EnableUI(float time); //UI�� ��� �� ����ϴ� �Լ�. DOFade�� ������ Ȱ��ȭ �ϰ� time���� Ȱ��ȭ�Ǵ� �ð��� �����ش�.
    
    public abstract void DisableUI(float time, Action action); //UI�� �� �� ����ϴ� �Լ�. DOFade�� ������ ��Ȱ��ȭ �ϰ� ���������� time���� ��Ȱ��ȭ �ð� �����ִµ�, Action�� �Ű������� �Ѱ� ��Ȱ��ȭ ���Ŀ� ������ �Ұ������� ������ �� �ִ�.
}
