using UnityEngine;

public class Player 
{
    PlayerTeam _team;
    string _name;
    int _score;
    int _hp;
    Vector2 _position;

    public Player(PlayerTeam v_team, string v_name) 
    {
        _team = v_team;
        _name = v_name;
        _score = 0;
        _hp = 3;
        _position = v_team.Spawn;
    }
    
    public PlayerTeam Team
    {
        get { return _team; }
        set { _team = value; }
    }
    
    public string Name 
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public int HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

}
    