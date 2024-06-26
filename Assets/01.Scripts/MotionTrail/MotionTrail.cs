using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeshTrailStruct
{
    public GameObject Container;

    public MeshFilter BodyMeshFilter;

    public Mesh BodyMesh;
}

public class MotionTrail : MonoBehaviour
{
    #region Variables & Initializer
    [Header("[PreRequisite]")]
    [SerializeField] private SkinnedMeshRenderer _sMRBody;

    private Transform _trailContainer;
    [SerializeField] private GameObject _meshTrailPrefab;
    private List<MeshTrailStruct> _meshTrailStructs = new List<MeshTrailStruct>();

    private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Vector3> _posMemory = new List<Vector3>();
    private List<Quaternion> _rotMemory = new List<Quaternion>();

    [Header("[Trail Info]")]
    [SerializeField] private int _trailCount;
    [SerializeField] private float _trailGap = 0.2f;
    [SerializeField][ColorUsage(true, true)] private Color _frontColor;
    [SerializeField][ColorUsage(true, true)] private Color _backColor;
    [SerializeField][ColorUsage(true, true)] private Color _frontColorInner;
    [SerializeField][ColorUsage(true, true)] private Color _backColorInner;

    #endregion

    #region MotionTrail

    void Start()
    {
        _trailContainer = transform.Find("Trail");

        for (int i = 0; i < _trailCount; i++)
        {
            MeshTrailStruct pss = new MeshTrailStruct();
            pss.Container = Instantiate(_meshTrailPrefab, _trailContainer);
            pss.BodyMeshFilter = pss.Container.transform.GetChild(0).GetComponent<MeshFilter>();

            pss.BodyMesh = new Mesh();

            _sMRBody.BakeMesh(pss.BodyMesh);

            pss.BodyMeshFilter.mesh = pss.BodyMesh;

            _meshTrailStructs.Add(pss);

            _bodyParts.Add(pss.Container);

            float alphaVal = (1f - (float)i / _trailCount) * 0.5f;
            pss.BodyMeshFilter.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", alphaVal);

            Color tmpColor = Color.Lerp(_frontColor, _backColor, (float)i / _trailCount);
            pss.BodyMeshFilter.GetComponent<MeshRenderer>().material.SetColor("_FresnelColor", tmpColor);

            Color tmpColor_Inner = Color.Lerp(_frontColorInner, _backColorInner, (float)i / _trailCount);
            pss.BodyMeshFilter.GetComponent<MeshRenderer>().material.SetColor("_BaselColor", tmpColor_Inner);
        }

        StartMotionTrail();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            StopCoroutine("BakeMeshCoroutine");

            StartMotionTrail();
        }
    }

    public void StartMotionTrail()
    {
        StartCoroutine("BakeMeshCoroutine");
    }

    public void EndMoionTrail()
    {
        StopCoroutine("BakeMeshCoroutine");
    }

    IEnumerator BakeMeshCoroutine()
    {
        for (int i = _meshTrailStructs.Count - 2; i >= 0; i--)
        {
            _meshTrailStructs[i + 1].BodyMesh.vertices = _meshTrailStructs[i].BodyMesh.vertices;

            _meshTrailStructs[i + 1].BodyMesh.triangles = _meshTrailStructs[i].BodyMesh.triangles;
        }

        _sMRBody.BakeMesh(_meshTrailStructs[0].BodyMesh);

        _posMemory.Insert(0, transform.position);
        _rotMemory.Insert(0, transform.rotation);

        if (_posMemory.Count > _trailCount)
            _posMemory.RemoveAt(_posMemory.Count - 1);
        if (_rotMemory.Count > _trailCount)
            _rotMemory.RemoveAt(_rotMemory.Count - 1);

        for (int i = 0; i < _bodyParts.Count; i++)
        {
            _bodyParts[i].transform.position = _posMemory[Mathf.Min(i, _posMemory.Count - 1)];
            _bodyParts[i].transform.rotation = _rotMemory[Mathf.Min(i, _rotMemory.Count - 1)];
        }

        yield return new WaitForSeconds(_trailGap);
        StartCoroutine("BakeMeshCoroutine");
    }
    #endregion
}