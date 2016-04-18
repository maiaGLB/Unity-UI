using UnityEngine;
using System.Collections.Generic;
using System;

public class MapRender : MonoBehaviour
{
    [SerializeField] private GameObject _playerPin;
    
    private Dictionary<string, GameObject> _playersIngame = new Dictionary<string, GameObject>();
    private MeshRenderer _mapRenderer;
    private Vector3 _extents;

    protected void Start()
    {
        Game.Instance.OnPlayerJoin += this.OnPlayerJoin;
        Game.Instance.OnPlayerDisconnect += this.OnPlayerDisconnect;
        Game.Instance.OnPlayerMove += this.OnPlayerMove;

        _mapRenderer = this.GetComponentInChildren<MeshRenderer>();
        _extents = _mapRenderer.bounds.extents;
    }

    private void PlayerPosition(MapRenderPinPlayer pin) 
    {
        float posX = (pin.Player.Position.x / 1000 * _extents.x * 2.0f) - _extents.x;
        float posY = (pin.Player.Position.y / 1000 * _extents.z * 2.0f) - _extents.z;
        
        pin.transform.localPosition = new Vector3(posX, 0.0f, posY);
    }

    private void OnPlayerMove(Player p)
    {
        if (_playersIngame.ContainsKey(p.Name))
        {
            return;
        }

        GameObject instance = _playersIngame[p.Name];

        MapRenderPinPlayer pin = instance.GetComponent<MapRenderPinPlayer>();
        pin.Player = p;

        PlayerPosition(pin);
    }

    private void OnPlayerJoin(Player p)
    {
        if (_playersIngame.ContainsKey(p.Name))
        {
            return;
        }
        
        GameObject instance = GameObject.Instantiate(_playerPin);
        instance.transform.SetParent(this.transform);

        MapRenderPinPlayer pin = instance.GetComponent<MapRenderPinPlayer>();
        pin.Player = p;

        PlayerPosition(pin);

        _playersIngame.Add(p.Name, instance);
    }

    private void OnPlayerDisconnect(string name)
    {
        if (_playersIngame.ContainsKey(name))
        {
            GameObject instance = _playersIngame[name];
            Destroy(instance);

            _playersIngame.Remove(name);
        }
    }
}
