using UnityEngine;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class MockGame : Game 
{
    private float _gameStartTime;
    private List<Event> _events = new List<Event>();

    internal class Event
    {
        public string type;
        public float time;
    }

    internal class JoinEvent : Event
    {
        public string playerName;
        public string playerTeam;
    }

    internal class MoveEvent : Event
    {
        public string to;
        public string playerName;
    }

    internal class ShootEvent : Event
    {
        public string target;
        public string playerName;
    }

    internal class DisconnectEvent : Event
	{
		public string playerName;
	}

    internal class SortEventByTime : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            if (x.time == y.time) return 0;
            else return x.time >= y.time ? 1 : -1;
        }
    }
     
	protected void Start()
    {
        _gameStartTime = Time.time;

        string readJson = System.IO.File.ReadAllText(System.IO.Path.Combine(Application.dataPath, "data.json"));

        JsonReader reader = new JsonReader(readJson);
        List<Dictionary<string, object>> output = reader.Read(typeof(List<Dictionary<string, object>>), false) as List<Dictionary<string, object>>;

        foreach (Dictionary<string, object> obj in output)
        {
			Event e = null;
            if (obj["type"].Equals("join"))
            {
                e = CreateJoinEvent(obj);
            }
            else if (obj["type"].Equals("move"))
            {
                e = CreateMoveEvent(obj);
            }
            else if (obj["type"].Equals("shoot"))
            {
                e = CreateShootEvent(obj);
            }
            else if (obj["type"].Equals("disconnect"))
			{
				e = CreateDisconnectEvent(obj);
			}
			if ( e != null ) 
			{ 
				_events.Add(e); 
			}
        }

        _events.Sort(new SortEventByTime());
    }

    protected void FixedUpdate()
    {
        float delta = Time.time - _gameStartTime;
        if (_events.Count == 0)
            return;

        Event e = _events[0];
        if (delta > e.time)
        {
            if (e.type == "join")
            {
                JoinEvent je = e as JoinEvent;
                AddPlayer(je.playerTeam, je.playerName);
            }
            else if (e.type == "move")
            {
                MoveEvent me = e as MoveEvent;
                MovePlayer(me.to, me.playerName);
            }
            else if (e.type == "shoot")
            {
                ShootEvent se = e as ShootEvent;
                ShootPlayer(se.target, se.playerName);
            }
            else if (e.type == "disconnect")
			{
				DisconnectEvent de = e as DisconnectEvent;
				RemovePlayer(de.playerName);
			}
            _events.Remove(e);
        }
    }

    private MoveEvent CreateMoveEvent(Dictionary<string, object> obj)
    {
        float time = float.Parse(obj["time"].ToString());

        MoveEvent me = new MoveEvent();
        me.type = obj["type"].ToString();
        me.time = time;
        me.to = obj["to"].ToString();
        me.playerName = obj["player"].ToString();

        return me;
    }

    private ShootEvent CreateShootEvent(Dictionary<string, object> obj)
    {
        float time = float.Parse(obj["time"].ToString());

        ShootEvent se = new ShootEvent();
        se.type = obj["type"].ToString();
        se.time = time;
        se.target = obj["target"].ToString();
        se.playerName = obj["player"].ToString();

        return se;
    }
    
    private JoinEvent CreateJoinEvent(Dictionary<string, object> obj)
    {
        float time = float.Parse(obj["time"].ToString());

        JoinEvent je = new JoinEvent();
        je.type = obj["type"].ToString();
        je.time = time;
        je.playerName = obj["name"].ToString();
        je.playerTeam = obj["team"].ToString();

        return je;
    }

    private DisconnectEvent CreateDisconnectEvent(Dictionary<string, object> obj)
    {
        float time = float.Parse(obj["time"].ToString());

        DisconnectEvent de = new DisconnectEvent();
        de.type = obj["type"].ToString();
        de.time = time;
        de.playerName = obj["name"].ToString();

        return de;
    }

}
