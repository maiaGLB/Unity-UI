using UnityEngine;
using System.Collections.Generic;
using System;
using Pathfinding.Serialization.JsonFx;

public class LoadDataJson : MonoBehaviour
{
    [Serializable]
    internal class Join
    {
        public string type;
        public string time;
        public string name;
        public string team;
    }

    [Serializable]
    internal class Move
    {
        public string type;
        public string time;
        public string to;
        public string player;
    }

    [Serializable]
    internal class Disconnect
    {
        public string type;
        public string time;
        public string name;
    }

    [Serializable]
    internal class Shoot
    {
        public string type;
        public string time;
        public string target;
        public string player;
    }

    [SerializeField] private List<Join> _joins = new List<Join>();
    [SerializeField] private List<Move> _moves = new List<Move>();
    [SerializeField] private List<Shoot> _shoots = new List<Shoot>();
    [SerializeField] private List<Disconnect> _disconnects = new List<Disconnect>();

    public void Execute() 
    {
        string readJson = System.IO.File.ReadAllText(System.IO.Path.Combine(Application.dataPath, "data.json"));
        JsonReader reader = new JsonReader(readJson);
        List<Dictionary<string, object>> output = reader.Read(typeof(List<Dictionary<string, object>>), false) as List<Dictionary<string, object>>;

        foreach (Dictionary<string, object> obj in output)
        {
            if (obj["type"].Equals("join"))
            {
                Join j = new Join();
                j.type = obj["type"].ToString();
                j.time = obj["time"].ToString();
                j.name = obj["name"].ToString();
                j.team = obj["team"].ToString();
                _joins.Add(j);
            }
            else if (obj["type"].Equals("move"))
            {
                Move m = new Move();
                m.type = obj["type"].ToString();
                m.time = obj["time"].ToString();
                m.to = obj["to"].ToString();
                m.player = obj["player"].ToString();
                _moves.Add(m);
            }
            else if (obj["type"].Equals("shoot"))
            {
                Shoot s = new Shoot();
                s.type = obj["type"].ToString();
                s.time = obj["time"].ToString();
                s.target = obj["target"].ToString();
                s.player = obj["player"].ToString();
                _shoots.Add(s);
            }
            else if (obj["type"].Equals("disconnect"))
            {
                Disconnect d = new Disconnect();
                d.type = obj["type"].ToString();
                d.time = obj["time"].ToString();
                d.name = obj["name"].ToString();
                _disconnects.Add(d);
            }
        }
    }

    void Awake () 
    {
        this.Execute();
    }
	
}
