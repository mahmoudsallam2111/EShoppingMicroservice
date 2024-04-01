using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    
    public class InternalServerrException : Exception
    {
        public string? Details { get; set; }
        public InternalServerrException(string message): base(message) { }
        public InternalServerrException(string message , string details): base(message)
        {
            Details = details;  
        }
        
    }
}
