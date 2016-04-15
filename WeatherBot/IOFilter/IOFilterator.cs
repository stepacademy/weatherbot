using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.IOFilter
{
    ///
    ///  IOFilterator
    ///
    public class IOFilterator
    {
        List<string> tokens = new List<string>();
        public void IncomeMessage(string message)
        {
            tokens.Clear();
            if (message.Length == 0)
                throw (new ErrorMessage("Пустая строка сообщения!"));

            tokens = BreakWords(message);
        }

        private string PrepareMessage(string message)
        {
            const string breaksymols = "!@#$%^&*()-+=:;_'?<>{}";
            string ret = new string(message.ToCharArray());
            foreach (char ch in breaksymols.ToCharArray())
                if (message.Contains(ch))
                    message = message.Replace(ch, ' ');
            return ret;
        }
        private List<string> BreakWords(string message)
        {
            message=PrepareMessage(message);
            string[] words = message.Split(' ');
            List<string> ret = new List<string>();

            foreach (string word in words)
                ret.Add(word);
            return ret;
        }
        public string OutcomeMessage()
        {
            string ret="";
            foreach (string str in tokens)
                ret = str +" "+ ret;
            return ret;
        }


    }

    public class ErrorMessage : SystemException
    {
        
        public string error { get; private set; }
            
        public ErrorMessage():base()
        {
        }
        public ErrorMessage(string message): base(message)  
        {
            error = message;
        }
        public ErrorMessage(string message, Exception innerException) : base(message, innerException)
        {

        }
        //protected ErrorMessage(SerializationInfo info, StreamingContext context)
        
    }
}
