using UnityEngine;
using System;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField] private PlayerTeam[] _teams;
    
    public delegate void OnPlayerJoinDelegate(Player p);
    public event OnPlayerJoinDelegate OnPlayerJoin;

    public delegate void OnPlayerDisconnectDelegate(string name);
    public event OnPlayerDisconnectDelegate OnPlayerDisconnect;
    
    public delegate void OnPlayerMoveDelegate(Player p);
    public event OnPlayerMoveDelegate OnPlayerMove;

    public delegate void OnPlayerShootDelegate();
    public event OnPlayerShootDelegate OnPlayerShoot;
    
    private List<Player> _players = new List<Player>();
    private Dictionary<string, Player> _playersOnline = new Dictionary<string, Player>();
    
    protected void Awake()
    {
        Game.Instance = this;
    }

    protected void MovePlayer(string to, string playerName)
    {
        if (_playersOnline.ContainsKey(playerName))
        {
            string search = ",";
            int firstPosition = to.IndexOf(search);

            if (firstPosition != -1) 
            {
                string x = to.Substring(0, firstPosition );
                string y = to.Substring(firstPosition+1);

                Debug.Log("x , y " + x + y);
                _playersOnline[playerName].Position = new Vector2(Convert.ToInt32(x), Convert.ToInt32(y));
            }
        }
        
        if (OnPlayerMove != null)
            OnPlayerMove(_playersOnline[playerName]);
    }

    protected void ShootPlayer(string target, string playerName)
    {
        if (_playersOnline.ContainsKey(playerName))
        {
            _playersOnline[playerName].Score += 1;
        }

        if (_playersOnline.ContainsKey(target))
        {
            if (_playersOnline[target].HP > 0)
            {
                _playersOnline[target].HP -= 1;
            }
            else 
            {
                KillPlayer(target);
            }
        }

        if (OnPlayerShoot != null)
            OnPlayerShoot();
    }

    public PlayerTeam GetTeam(string name)
    {
        foreach(PlayerTeam team in _teams)
        {
            if (team.Name == name)
                return team;
        }

        return null;
    }
    
    protected void AddPlayer(string teamName, string name)
    {
        PlayerTeam team = this.GetTeam(teamName);
        if(team == null)
        {
            Debug.LogWarning("Invalid team specified: " + teamName);
            return;
        }
        
        Player player = new Player(team, name);
        
        _playersOnline.Add(name, player);

        if (OnPlayerJoin != null)
            OnPlayerJoin(player);
    }

    protected void RemovePlayer(string name)
    {
        if (_playersOnline.ContainsKey(name))
        {
            _playersOnline.Remove(name);
        }

        if (OnPlayerDisconnect != null)
            OnPlayerDisconnect(name);
    }

    private void KillPlayer(string playerDead)
    {
Debug.Log(" ************* player dead " + playerDead);
        if (_playersOnline.ContainsKey(playerDead))
        {
            _playersOnline.Remove(playerDead);
        }
    }
       
}
