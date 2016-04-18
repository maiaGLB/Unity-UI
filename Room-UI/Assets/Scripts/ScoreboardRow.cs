using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreboardRow : MonoBehaviour 
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _name;

    public Player Player { get; set; }

    protected void Update()
    {
        _name.text = this.Player.Name;
        _score.text = this.Player.Score.ToString();
    }
}
