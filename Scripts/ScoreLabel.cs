using Godot;
using System;

public partial class ScoreLabel : Label
{
	private int _score = 0;
    private bool isVegasMode;

    	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var globalVar = GetNode<global_variables>("/root/GlobalVariables");
		isVegasMode = globalVar.isVegasMode;
		GD.Print("Vegas mode is set to: " + isVegasMode);
        _score = isVegasMode ? -52 : 0;
        UpdateScoreText();
	}

	//Called when a card is successfully moved to an Ace zone
	public void OnCardMoveToAceZone()
	{
		_score = isVegasMode ? _score + 5 : _score + 10;
		UpdateScoreText();
	}

    // Called when a card is moved from the deck to a KingZone
    public void OnCardMoveToKingZone()
    {
        _score = isVegasMode ? _score : _score + 5;
        UpdateScoreText();
    }

    // Called when a card is turned face-up in a KingZone 
    public void OnCardTurnedFaceUp()
    {
        _score = isVegasMode ? _score : _score + 5;
        UpdateScoreText();
    }

    // Called when a card is moved from one KingZone to another
    public void OnCardMoveBetweenKingZones()
    {
        _score = isVegasMode ? _score : _score + 3;
        UpdateScoreText();
    }

    // Called when a card is moved from a suit stack to a KingZone
    public void OnCardMoveFromAceZoneToKingZone()
    {
        _score = isVegasMode ? _score : _score - 15;
        UpdateScoreText();
    }

	 // Update the score text
    private void UpdateScoreText()
    {
		GD.Print("Update Score");
        Text = $"Score: {_score}";
    }
}
