using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitingPenguin : MonoBehaviour
{
    Animator _animator;

    [SerializeField]
    private GameObject _torch;

    private Material _ropeMat;

    private readonly float minRopeMatcutoffValue = 4.35f;
    private readonly float maxRopeMatcutoffValue = 4.75f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _ropeMat = transform.root.Find("rope").GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SetGetTourchAnimation();
        }
    }

    public void SetGetTourchAnimation()
    {
        _animator.SetTrigger("GetTorchTrigger");
    }

    public void GetTorch()
    {
        _torch.SetActive(true);
    }

    public void UseTorch()
    {
        _torch.SetActive(true);
        StartCoroutine(SetRopeBurningMat());
    }

    private IEnumerator SetRopeBurningMat()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 3f)
        {
            float t = (Time.time - startTime) / 3f;
            _ropeMat.SetFloat("_CutoffValue", Mathf.Lerp(maxRopeMatcutoffValue, minRopeMatcutoffValue, t));
            yield return new WaitForEndOfFrame();
        }

        _ropeMat.SetFloat("_CutoffValue", minRopeMatcutoffValue);
    }
}
