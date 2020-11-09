using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TwitchController : MonoBehaviour {

	public TwitchIRC TwitchIRC;
	[SerializeField, SerializeReference]
	public GameObject[] obstacles;
	

	

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
			case "fire all": StartCoroutine(FireAllTime()); break;
			
        }
	}

	#region TwitchActions
	private void ActivateObstacles()
    {

		foreach(GameObject go in obstacles)
        {
			if (go != null)
			{
				IObstacle iobstacle = go.GetComponent<IObstacle>();
				iobstacle.Activate();
			}
        }

    }

	private IEnumerator FireAllTime()
    {
		while (true)
		{
			yield return new WaitForSeconds(0.05f);
			ActivateObstacles();
		}
    }



	private void Cheer()
    {
		LevelController.Instance.SoundController.PlayMusic(MusicType.Cheering);
    }
	#endregion


}
