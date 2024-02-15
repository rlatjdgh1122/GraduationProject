using System;
using UnityEngine;

public abstract class NormalUI : MonoBehaviour
{
    public UIType UIType;

    public virtual void Awake()
    {

    }

    public abstract void EnableUI(float time, object obj); //UI를 띄울 때 사용하는 함수. DOFade로 서서히 활성화 하고 time으로 몇초동안 활성화할건지 정해준다.
    
    public abstract void DisableUI(float time, Action action); //UI를 끌 때 사용하는 함수. DOFade로 서서히 비활성화 하고 마찬가지로 time으로 비활성화 시간 정해주는데, Action도 매개변수로 넘겨 비활성화 이후에 무엇을 할것인지도 정해줄 수 있다.
}
