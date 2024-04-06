using Godot;
using System;

public partial class ScoreLabel : Label
{
	private int _score = 0;

	//Called when a card is successfully moved to an Ace zone
	public void OnCardMoveToAceZone()
	{
		_score += 10;
		UpdateScoreText();
	}

    // Called when a card is moved from the deck to a KingZone
    public void OnCardMoveToKingZone()
    {
        _score += 5;
        UpdateScoreText();
    }

    // Called when a card is turned face-up in a KingZone 
    public void OnCardTurnedFaceUp()
    {
        _score += 5;
        UpdateScoreText();
    }

    // Called when a card is moved from one KingZone to another
    public void OnCardMoveBetweenKingZones()
    {
        _score += 3;
        UpdateScoreText();
    }

    // Called when a card is moved from a suit stack to a KingZone
    public void OnCardMoveFromAceZoneToKingZone()
    {
        _score -= 15;
        UpdateScoreText();
    }

	 // Update the score text
    private void UpdateScoreText()
    {
        Text = $"Score: {_score}";
    }
}
