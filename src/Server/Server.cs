using System;
using System.Text;
using System.Text.RegularExpressions;
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
            AddPipe(new EncodingPipe());
            Route("/", ctx => ctx.Response.End("Server"));
        }

        public void GetCallerName(IManosContext ctx, string number, string prependWith, string appendWith)
        {
            var callerId = GetCallerIdByNumber(number);

            ctx.Response.End(!string.IsNullOrEmpty(callerId) ? prependWith + callerId + appendWith : CleanNumber(number));
        }

        public void GetCallerNameReversed(IManosContext ctx, string number, string prependWith, string appendWith)
        {
            var callerId = GetCallerIdByNumber(number);

            ctx.Response.End(!string.IsNullOrEmpty(callerId) ? prependWith + new string(callerId.Reverse().ToArray()) + appendWith : CleanNumber(number));
        }

        private string GetCallerIdByNumber(string number)
        {
            using (var session = Global.Store.OpenSession())
                return (from cnam in session.Query<CallerId>()
                        where cnam.PhoneNumber == CleanNumber(number)
                        select cnam.Name).FirstOrDefault();

        }

        private string CleanNumber(string number)
        {
            return Regex.Replace(number, "[^0-9]", "");
        }

        public void AddUpdateCNam(IManosContext ctx)
        {
            ctx.Response.End(@"
<!DOCTYPE HTML>
<html>
<head>
<meta charset='utf-8'>
<title>CNAM Add/Update</title></head>
<body>
<form action='AddCallerIdToSystem' method='post'>
  <label for='name'>Name</label>
  <input type='text' name='name' id='name'>
  <label for='number'>Number</label>
  <input type='text' name='number' id='number'>
<input type='submit' name='button' id='button' value='Save'>
</form>
</body>
</html>
");
        }

        public void AddCallerIdToSystem(IManosContext ctx, string number, string name)
        {
            using (var session = Global.Store.OpenSession())
            {
                string cleanNumber = CleanNumber(number);
                var callerId = session.Query<CallerId>().Where(x => x.PhoneNumber == cleanNumber).FirstOrDefault();
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
                ctx.Response.End(name + " " + cleanNumber);
            }
        }
    }
}