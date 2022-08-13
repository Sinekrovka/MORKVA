using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using StarterAssets;
using UnityEngine;

public class CameraFollowController : GameSystem, IUpdating, IIniting
{
    private Transform _cam;
    private Transform _player;
    public void OnInit()
    {
        _cam = FindObjectOfType<Camera>().transform;
        _player = FindObjectOfType<ThirdPersonController>().transform;
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void OnUpdate()
    {
        _cam.position = new Vector3(_player.position.x, _cam.position.y, _player.position.z-25);
    }
}
