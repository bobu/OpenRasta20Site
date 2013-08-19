using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenRasta;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace OpenRasta20Site
{
    public class ErrorCheckingContributor : IPipelineContributor
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner
                .Notify(CheckRequestDecoding)
                .After<KnownStages.IOperationResultInvocation>()
                .And.Before<KnownStages.ICodecResponseSelection>();
        }

        private static PipelineContinuation CheckRequestDecoding(ICommunicationContext context)
        {
            if (context.ServerErrors.Count == 0)
            {
                return PipelineContinuation.Continue;
            }

            Error err = context.ServerErrors[0];

            // Get a suitable message (err.Message contains stack traces, so try to avoid that)
            string msg = err.Title;
            if (msg == null && err.Exception != null)
                msg = err.Exception.Message;
            if (msg == null)
                msg = err.Message;

            // Create instance of an error information resource which is specific for the application
            // - This one is rather simple and only contains a copy of the message
            ApplicationError error = new ApplicationError(msg);

            // Set operation result to be "400 Bad Request" and remove errors
            context.OperationResult = new OperationResult.BadRequest { ResponseResource = error };
            context.ServerErrors.Clear();

            // Render immediately without starting any handlers
            return PipelineContinuation.RenderNow;
        }
    }

    public class ApplicationError
    {
        public string Message { get; set; }

        public ApplicationError(string message)
        {
            Message = message;
        }
    }
}