using System;
using UnityEngine;

[Serializable]
public class PlayerTeam
{
    [SerializeField] private Color _colorTeam;
    [SerializeField] private string _name;
    [SerializeField] private Vector2 _spawn;
        
    public PlayerTeam(string name, Vector2 spawn, Color color)
    {
        _name = name;
        _spawn = spawn;
        _colorTeam = color;
    }

    public string Name
    {
        get { return _name; }
    }

    public Vector2 Spawn
    {
        get { return _spawn; }
    }

    public Color ColorTeam
    {
        get { return _colorTeam; }
    }


}
