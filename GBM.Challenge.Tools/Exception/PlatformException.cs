using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace GBM.Challenge.Tools.Exception
{
    [Serializable]
    public class PlatformException : System.Exception, ISerializable
    {
        public PlatformException(string message)
            : base(message)
        {
        }

        public PlatformException(string message, System.Exception innerException)
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
