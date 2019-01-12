using System.Diagnostics;
using System.Text;

namespace GBM.Challenge.Tools.Logger
{
    public static class EventManager
    {
        public static void WriteEvent(System.Exception exception)
        {
            var saved = false;
            var attempts = 0;
            while (!saved && attempts < 3)
            {
                try
                {
                    Trace.TraceError(GetExceptionBlock(exception), "Error");
                    saved = true;
                }
                catch (System.Exception)
                {

                }

                attempts++;
            }
        }

        public static void WriteEvent(string message, EventLevel eventLevel = EventLevel.Informative, int eventId = 1000)
        {
            var saved = false;
            var attempts = 0;
            while (!saved && attempts < 3)
            {
                try
                {
                    if (EventIsWritten(eventLevel))
                    {
                        switch (eventLevel)
                        {
                            case EventLevel.Error:
                                Trace.TraceError(GetEventHead(eventLevel, eventId) + message);
                                break;
                            case EventLevel.Warning:
                                Trace.TraceWarning(GetEventHead(eventLevel, eventId) + message);
                                break;
                            case EventLevel.Informative:
                                Trace.TraceInformation(GetEventHead(eventLevel, eventId) + message);
                                break;
                            default:
                                Trace.TraceError(GetEventHead(eventLevel, eventId) + message);
                                break;
                        }
                    }

                    saved = true;
                }
                catch (System.Exception)
                {

                }

                attempts++;
            }
        }
        
        private static string GetExceptionBlock(System.Exception exception)
        {
            var exceptionString = new StringBuilder();
            exceptionString.AppendLine("Excepcion Externa.");
            exceptionString.AppendLine("Tipo: " + exception.GetType().Name);
            exceptionString.AppendLine("Ayuda: " + exception.HelpLink);
            exceptionString.AppendLine("Mensaje: " + exception.Message);
            exceptionString.AppendLine("Objeto Origen: " + exception.Source);
            exceptionString.AppendLine("Metodo Origen: " + exception.TargetSite);
            exceptionString.AppendLine("StackTrace: " + exception.StackTrace);

            var internalException = exception;
            while (internalException.InnerException != null)
            {
                internalException = internalException.InnerException;
                exceptionString.AppendLine();
                exceptionString.AppendLine("Excepcion Interna.");
                exceptionString.AppendLine("Tipo: " + internalException.GetType().Name);
                exceptionString.AppendLine("Ayuda: " + internalException.HelpLink);
                exceptionString.AppendLine("Mensaje: " + internalException.Message);
                exceptionString.AppendLine("Origen: " + internalException.Source);
                exceptionString.AppendLine("Metodo: " + internalException.TargetSite);
                exceptionString.AppendLine("StackTrace: " + internalException.StackTrace);
            }

            return exceptionString.ToString();
        }

        private static string GetEventHead(EventLevel eventLevel, int eventId = 1000)
        {
            var headString = new StringBuilder();
            headString.AppendLine("Fecha: " + Kit.GetDateTime());
            headString.AppendLine("Nivel: " + eventLevel.ToString());
            headString.AppendLine("ID Evento: " + eventId.ToString());
            headString.AppendLine();

            return headString.ToString();
        }

        private static bool EventIsWritten(EventLevel eventLevel)
        {
            var eventLevelMinimo = 3;

            return (int)eventLevel <= eventLevelMinimo;
        }
    }

    public enum EventLevel
    {
        Informative = 3,
        Warning = 2,
        Error = 1
    }
}
