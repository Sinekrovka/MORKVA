using System;
using System.Collections;
using Kuhpik;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class NoiseController : GameSystem, IIniting,IUpdating
{
    [SerializeField] private int maxNoiseLevel;
    [Space]
    [SerializeField] private int addNoiseForStop;
    [SerializeField] private float timeWaitOnStop;
    [Space]
    [SerializeField] private int addNoiseForMoving;
    [SerializeField] private float timeWaitMoving;
    [Space]
    [SerializeField] private Image noiselevelImage;

    public Action<Vector3, bool> _maxNoise;
    private ThirdPersonController _thirdPerson;
    private float _speedPlayer;
    private bool checkMovement;
    private float currentNoiseLevel;
    public void OnInit()
    {
        _thirdPerson = FindObjectOfType<ThirdPersonController>();
        checkMovement = false;
        currentNoiseLevel = 0;
    }

    public void OnUpdate()
    {
        _speedPlayer = _thirdPerson.GetCurrentSpeed;
        if (_speedPlayer > 0 && !checkMovement)
        {
            checkMovement = true;
            StopAllCoroutines();
            StartCoroutine(AddNoise(addNoiseForMoving, timeWaitMoving));
        }
        else
        {
            checkMovement = false;
            StopAllCoroutines();
            StartCoroutine(AddNoise(addNoiseForStop, timeWaitOnStop));
        }
    }

    private IEnumerator AddNoise(int countPoints, float time)
    {
        yield return new WaitForSeconds(time);
        currentNoiseLevel += 1f*countPoints / maxNoiseLevel;
        noiselevelImage.fillAmount = currentNoiseLevel;
        if (currentNoiseLevel >= maxNoiseLevel)
        {
            _maxNoise?.Invoke(_thirdPerson.transform.position, true);
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine(AddNoise(countPoints, time));
        }
    }
}
