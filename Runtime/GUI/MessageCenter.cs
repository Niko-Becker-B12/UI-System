using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.Utilities;

public class MessageCenter : Singleton<MessageCenter>
{

    //public TextMeshProUGUI tableHeader;

    public List<Message> messages = new List<Message>();

    public GameObject messagePrefab;
    public RectTransform messageParent;

    public Color messageColor;
    public Color warningColor;
    public Color errorColor;

    public CanvasGroup notificationBubbleCanvasGroup;
    public TextMeshProUGUI notificationText;

    int _unreadMessages;

    public int UnreadMessages
    {
        get
        {
            return _unreadMessages;
        }
        set
        {
            _unreadMessages = value;
            DisplayNotification();
        }
    }


    private void Start()
    {

        DisplayNotification();

    }

    public void ClearNotifications()
    {

        UnreadMessages = 0; 

    }

    public void GenerateNewMessage(string message, MessageType messageType, string sender = "Guide Tablet")
    {

        UnreadMessages++;

        GameObject newMsg = Instantiate(messagePrefab, messageParent);

        if(newMsg.GetComponentInChildren(typeof(TextMeshProUGUI)))
        {

            TextMeshProUGUI text = (TextMeshProUGUI)newMsg.GetComponentInChildren(typeof(TextMeshProUGUI));

            string time = System.DateTime.Now.ToString("HH:mm:ss");

            message = message.Replace("\n", "\n<indent=512>");

            switch (messageType)
            {

                case MessageType.message:
                    text.text = $"<color=#000000>{time}<indent=256>{sender}<indent=512><color=#{messageColor.ToHexString()}>{message}";
                    break;
                case MessageType.warning:
                    text.text = $"<color=#000000>{time}<indent=256>{sender}<indent=512><color=#{warningColor.ToHexString()}>{message}";
                    break;
                case MessageType.error:
                    text.text = $"<color=#000000>{time}<indent=256>{sender}<indent=512><color=#{errorColor.ToHexString()}>{message}";
                    break;

            }

        }

    }

    public void GenerateNewCommand(string id, string[] args, string sender = "Guide Tablet")
    {

        Message newMessageData = messages.Find(x => x.id == id);

        if (newMessageData == null)
            return;

        string message = newMessageData.messageText.GetLocalizedString();

        UnreadMessages++;

        GameObject newMsg = Instantiate(messagePrefab, messageParent);

        if (newMsg.GetComponentInChildren(typeof(TextMeshProUGUI)))
        {

            TextMeshProUGUI text = (TextMeshProUGUI)newMsg.GetComponentInChildren(typeof(TextMeshProUGUI));

            string time = System.DateTime.Now.ToString("HH:mm:ss");

            message = message.Replace("\n", "\n<indent=512>");
            message = string.Format(message, args);

            switch (newMessageData.messageType)
            {

                case MessageType.message:
                    text.text = $"<color=#000000>{time}<indent=256>{sender}<indent=512><color=#{messageColor.ToHexString()}>{message}";
                    break;
                case MessageType.warning:
                    text.text = $"<color=#000000>{time}<indent=256>{sender}<indent=512><color=#{warningColor.ToHexString()}>{message}";
                    break;
                case MessageType.error:
                    text.text = $"<color=#000000>{time}<indent=256>{sender}<indent=512><color=#{errorColor.ToHexString()}>{message}";
                    break;

            }


        }
    }

    public void DisplayNotification()
    {

        if(UnreadMessages > 0 && UnreadMessages <= 9)
        {

            notificationBubbleCanvasGroup.alpha = 1;

            notificationText.text = UnreadMessages.ToString();
        }
        else if(UnreadMessages > 9)
        {

            notificationBubbleCanvasGroup.alpha = 1;

            notificationText.text = "9+";

        }
        else
        {

            notificationBubbleCanvasGroup.alpha = 0;

            notificationText.text = "0";

        }

    }

}

public enum MessageType
{

    message,
    warning,
    error

}

[System.Serializable]
public class Message
{

    public LocalizedString messageText;
    public MessageType messageType;

    public string id;

}