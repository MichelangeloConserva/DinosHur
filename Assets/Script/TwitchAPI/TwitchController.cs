using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchController : MonoBehaviour {

	public TwitchIRC TwitchIRC;
	[SerializeField, SerializeReference]
	public List<IObstacle> obstacles;
	

	

	void Start () {

		EnableTwitch();
        SendMsg("Hello this is unity speaking:\nWrite FIRE to fire a cannonball.\n Or CHEER to cheer for the racers");	

	}

	public void EnableTwitch()
    {
		TwitchIRC.ConnectToTwitch();
		TwitchIRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
	}
	
	void OnChatMsgRecieved(string msg)
	{
		int msgIndex = msg.IndexOf("PRIVMSG #"); 
		string content = msg.Substring(msgIndex + TwitchIRC.channelName.Length + 11);
		ParseMessageRecieved(content);
	}

	public void SendMsg(string msg){
		TwitchIRC.SendMsg(msg);
	}

	public void ParseMessageRecieved(string msg){

		msg = msg.ToLower();
		switch (msg)
        {
			case "fire": ActivateObstacles();  break;
			case "cheer": Cheer();  break;
			
        }
	}

	#region TwitchActions
	private void ActivateObstacles()
    {
		obstacles.ForEach(o => o.Activate());
    }

	private void Cheer()
    {
		LevelController.Instance.SoundController.PlayMusic(MusicType.Cheering);
    }
	#endregion


}
