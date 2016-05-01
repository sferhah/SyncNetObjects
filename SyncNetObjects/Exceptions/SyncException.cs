using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferhah.SyncNetObjects.Exceptions
{
    public class SyncException : Exception
    {
        public String ErrorSource { get; private set; }
        public String ErrorMessage { get; private set; }
        

        public SyncException(string source, string message)
            : base(ToMessage(source, message))
        {
            ErrorSource = source;
            ErrorMessage = message;
        }

        public static String ToMessage(string source, string message)         
        {   
            return "source : " + source + " message: "+ message;
        }
    }

  

 
}
