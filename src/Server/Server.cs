using System;
using Domain;
using Manos;
using Raven.Client.Linq;
using System.Linq;

namespace Server
{
    public class Server : ManosApp
    {
        public Server()
        {
            new Global();
            Route("/", ctx => ctx.Response.End("CNAM Server"));
        }

        public void GetCallerName(IManosContext ctx, string number)
        {
            using (var session = Global.DocumentStore.OpenSession())
            {
                var callerId = (from cnam in session.Query<CallerId>()
                                where cnam.PhoneNumber == number.Trim()
                                select cnam.Name).FirstOrDefault();


                ctx.Response.End(string.IsNullOrEmpty(callerId) ? callerId : number);
            }
        }

        public void AddCallerIdToSystem(IManosContext ctx, string number, string name)
        {
            using (var session = Global.DocumentStore.OpenSession())
            {
                var callerId = session.Query<CallerId>().Where(x => x.PhoneNumber == number).FirstOrDefault();
                if (callerId == null)
                    session.Store(new CallerId
                                      {
                                          Name = name,
                                          PhoneNumber = number,
                                          LastUpdated = DateTime.UtcNow
                                      });
                else
                {
                    callerId.Name = name;
                    callerId.LastUpdated = DateTime.UtcNow;
                }

                session.SaveChanges();

                ctx.Response.End("Done");
            }
        }
    }
}