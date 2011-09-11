using System;
using System.Text;
using Manos;

namespace Server
{
    public class EncodingPipe : ManosPipe
    {
        public override void OnPreProcessRequest(ManosApp app, Manos.Http.IHttpTransaction transaction, Action complete)
        {
            transaction.Response.ContentEncoding = Encoding.UTF8;
            //transaction.Response.Headers.SetHeader("Content-Type", "text/plain; charset=UTF-8");
            //transaction.Response.Headers.SetHeader("Content-Language", "en");
            transaction.Response.Headers.ContentEncoding = Encoding.UTF8;
            complete();
        }
    }
}