using Microsoft.Bot.Connector.DirectLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botservice
{
    class DirectLineInterface
    {
        string directLineSecret;
        string botId;
        string fromUser;
        Microsoft.Bot.Connector.DirectLine.DirectLineClient _directLineInterface;

        Conversation _conversation;
        string _watermark;



        public DirectLineInterface()
        {
            directLineSecret = "riTZZ4HuoJI.F_WMKsaC9fEKb77W_5wk1v8LZo12ZQa_5GlkIv9P5BE";
            botId = "testbotqatr-bot";
            fromUser = "testuser";
            _directLineInterface = new Microsoft.Bot.Connector.DirectLine.DirectLineClient(directLineSecret);
        }

        public static implicit operator DirectLineClient(DirectLineInterface v)
        {
            throw new NotImplementedException();
        }

        public Conversation StartConversation()
        {
            _conversation = _directLineInterface.Conversations.StartConversation();
            return _conversation;
        }

        public ResourceResponse SendMessage(string message)
        {
            var fromProperty = new ChannelAccount(fromUser);
            var activity = new Activity(text: message, fromProperty: fromProperty, type: "message");
            return _directLineInterface.Conversations.PostActivity(_conversation.ConversationId, activity);
        }

        public IList<Activity> GetMessages()
        {
            var response = _directLineInterface.Conversations.GetActivities(_conversation.ConversationId, _watermark);
            _watermark = response.Watermark;
            return response.Activities;
        }

    }

    }
