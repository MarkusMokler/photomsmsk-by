using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAuthTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            var token = GetToken("breate", "ctf-face3");
            Console.WriteLine(token);
            Console.ReadKey();
        }

        static string GetToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ), 
                            new KeyValuePair<string, string>( "username", userName ), 
                            new KeyValuePair<string, string> ( "Password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync("http://localhost:11551//api/token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
