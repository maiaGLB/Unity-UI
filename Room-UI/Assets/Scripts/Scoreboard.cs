using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Scoreboard : MonoBehaviour 
{
    [Serializable]
    internal class Team
    {
        public string team;
        public Color color;
        public RectTransform group;
    }

    [SerializeField] private GameObject _templatePlayer;
    [SerializeField] private Team[] _teams;
    
    private Dictionary<string, GameObject> _playersIngame = new Dictionary<string, GameObject>();

    protected void Awake()
    {
        _templatePlayer.SetActive(false);
    }

	protected void Start () 
    {
        Game.Instance.OnPlayerJoin += this.OnPlayerJoin;
        Game.Instance.OnPlayerDisconnect += this.OnPlayerDisconnect;
    }

    private Scoreboard.Team GetTeamDefinition(string team)
    {
        foreach(Scoreboard.Team t in _teams)
        {
            if (t.team == team)
                return t;
        }

        return null;
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

    private void OnPlayerJoin(Player p)
    {
        if (_playersIngame.ContainsKey(p.Name))
        {
            return;
        }
        
        Team team = this.GetTeamDefinition(p.Team.Name);

        GameObject instance = GameObject.Instantiate(_templatePlayer);
        instance.transform.SetParent(team.group, false);
        instance.GetComponent<Image>().color = team.color;

        ScoreboardRow row = instance.GetComponent<ScoreboardRow>();
        row.Player = p;
        instance.SetActive(true);

        _playersIngame.Add(p.Name, instance);
    }
}
