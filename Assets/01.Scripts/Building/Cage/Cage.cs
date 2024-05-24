using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cage : PoolableMono
{
    [SerializeField]
    private PenguinTypeEnum _penguinType;

    [SerializeField]
    private ParticleSystem _clickParticle;

    [SerializeField]
    private float _disolveDuration = 1.7f;

    private PenguinStoreUI _spawnUI;
    private MeshRenderer _visualRenderer;
    private CagePenguin _currentPenguin;
    private Transform _spawnTrm;

    private bool _isClick = false;

    private void Awake()
    {
        _visualRenderer = transform.Find("Visual").GetComponent<MeshRenderer>();
        _spawnTrm = transform.Find("PenguinPos").GetComponent<Transform>();

        _spawnUI = FindObjectOfType<PenguinStoreUI>();

        CagePenguin[] _penguins = Resources.LoadAll<CagePenguin>("CagePenguin");

        foreach (var penguin in _penguins.Where(penguin => penguin.PenguinType == _penguinType))
        {
            _currentPenguin = Instantiate(penguin);
            _currentPenguin.transform.SetParent(_spawnTrm);
            _currentPenguin.transform.localPosition = Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        if (!_isClick && !WaveManager.Instance.IsBattlePhase && !TutorialManager.Instance.ShowTutorialQuest)
        {
            SignalHub.OnDefaultBuilingClickEvent.Invoke();

            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);

            StartCoroutine(DissolveCoroutine());

            _isClick = true;
        }
    }

    private IEnumerator DissolveCoroutine() //값 일단 임시
    {
        _visualRenderer.material.DOFloat(-5, "_DissolveHeight", _disolveDuration);

        yield return new WaitForSeconds(1f);

        _spawnUI.UnlockSlot(_penguinType);
        _currentPenguin.DestroyCage();

        yield return new WaitForSeconds(4f);

        _clickParticle.Play();

        yield return new WaitForSeconds(0.5f);

        Destroy(this);
    }
}
