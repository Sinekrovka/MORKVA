using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class CameraFollowController : GameSystem, IUpdating, IIniting
{
    private Transform _cam;
    private Transform _player;
    public void OnInit()
    {
        _cam = FindObjectOfType<Camera>().transform;
        _player = GameObject.FindWithTag("Player").transform;
    }

    public void OnUpdate()
    {
        _cam.position = new Vector3(_player.position.x, _cam.position.y, _player.position.z-25);
    }
}
