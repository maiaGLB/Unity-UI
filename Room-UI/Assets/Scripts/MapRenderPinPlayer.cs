using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapRenderPinPlayer : MonoBehaviour 
{
    [SerializeField] private TextMesh _textPlayer;
    [SerializeField] private MeshRenderer _colorPlayer;
        
    public Player Player { get; set; }

    protected void Update()
    {
        _textPlayer.text = this.Player.Name;
        _colorPlayer.material.color = this.Player.Team.ColorTeam;
    }
}
