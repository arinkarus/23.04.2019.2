using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PseudoLINQ
{
    public class ComparerNotFound : Exception
    {
        public ComparerNotFound()
        {
        }

        public ComparerNotFound(string message) : base(message)
        {
        }

        public ComparerNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComparerNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
