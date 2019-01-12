using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace GBM.Challenge.Tools.Exception
{
    [Serializable]
    public class LogicException : System.Exception, ISerializable
    {
        public LogicException(string message)
            : base(message)
        {
        }

        public LogicException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
