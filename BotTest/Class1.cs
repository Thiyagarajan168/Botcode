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
        int count;
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

        public async Task<Conversation> StartConversationAsync()
        {
            _conversation = await _directLineInterface.Conversations.StartConversationAsync();
            count = 0;

            return _conversation;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            count++;
            var fromProperty = new ChannelAccount(fromUser);
            Activity userMessage = new Activity
            {
                From = fromProperty,
                Text = message,
                Type = ActivityTypes.Message
            };
          //  var activity = new Activity(text: message, fromProperty: fromProperty, type: "message");
           var requestdetails= await _directLineInterface.Conversations.PostActivityAsync(_conversation.ConversationId, userMessage);
            
           
            var response = await _directLineInterface.Conversations.GetActivitiesAsync(_conversation.ConversationId, _watermark);
            if(count==1)
            {
                return response.Activities[0].Text;
            }
            else
            {
                var responseActivity=response.Activities.Where(x => x.ReplyToId == requestdetails.Id).Select(x=>x.Text).FirstOrDefault();
                return responseActivity;

            }

            return response.Activities.ToString();
        }

        public IList<string> GetMessages()
        {
            var response = _directLineInterface.Conversations.GetActivities(_conversation.ConversationId, _watermark);
            _watermark = response.Watermark;
            List<string> messages = new List<string>();
            foreach(Activity a in response.Activities)
            {
                messages.Add(a.Text);
            }
            return messages;
        }

    }

}
