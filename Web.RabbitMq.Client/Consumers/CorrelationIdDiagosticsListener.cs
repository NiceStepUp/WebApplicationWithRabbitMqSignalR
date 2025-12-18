using System.Diagnostics;

namespace Web.RabbitMq.Client.Consumers;

// This class should be placed in some logging library. So this class is created
// here to compile library 
public class CorrelationIdDiagosticsListener : DiagnosticListener
{
    public CorrelationIdDiagosticsListener()
        : base("My.CorrelationId")
    {
    }

    public void Write(string correlationId)
    {
        string correlationIdSetupName = "My.CorrelationId.Setup";
        if (IsEnabled(correlationIdSetupName))
        {
            Write(
                correlationIdSetupName,
                new
                {
                    CorrelationId = correlationId
                });
        }
    }
}