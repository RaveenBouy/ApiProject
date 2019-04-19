using Client.Models;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Client.Classes
{
    public class UserProcessor
    {
        public AuthResponseModel Login(string username, string password)
        {
            var response = new AuthResponseModel();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.Login +
                $"?username={username}",
                $"&password={password}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    response.Response = (int)json["response"];
                    response.Status = json["status"].ToString();

                    if (response.Response.Equals(200))
                    {
                        response.Token = json["token"].ToString();
                        response.Value = (int)json["value"];
                        ReferenceList.Token = response.Token;
                    }
                    else
                    {
                        response.Info = json["info"].ToString();
                    }

                    return response;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<AuthResponseModel> RegisterAsync(string username, string password, int userType)
        {
            string myJson = "{'UserName': '" + username + "', 'Email' : 'user@email.com', 'Pass':'" + password + "', 'UserType' : " + userType + "}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PostAsync(
                            ReferenceList.Register,
                            new StringContent(myJson, Encoding.UTF8, "application/json"));

                json = await post.Content.ReadAsStringAsync();
            }

            try
            {
                json = JsonMapper.ToObject(json.ToString());
                response.Response = (int)json["response"];
                response.Status = json["status"].ToString();
                response.Info = json["info"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
            }

            return response;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            JsonData json = new JsonData();
            var users = new List<UserModel>();

            using (var client = new HttpClient())
            {
                var get = await client.GetAsync(
                            ReferenceList.StaffUsers +
                            $"{ReferenceList.Token}");

                using (var streamReader = new StreamReader(await get.Content.ReadAsStreamAsync()))
                {
                    json = JsonMapper.ToObject(streamReader.ReadToEnd());
                }
            }

            try
            {
                for (int i = 0; i < json.Count; i++)
                {
                    users.Add(new UserModel
                    {
                        Id = (int)json[i]["id"],
                        UserName = json[i]["userName"].ToString(),
                        Email = json[i]["email"].ToString(),
                        UserType = (int)json[i]["userType"],
                        IsVerified = (bool)json[i]["isVerified"],
                        DateCreated = json[i]["dateCreated"].ToString()
                    });
                }

                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AuthResponseModel> UpdateUser(int id, string type, string value)
        {
            string myJson = "{'id': '" + id + "', 'type' : '" + type + "', 'value':'" + value + "', 'authToken' : '" + ReferenceList.Token + "'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PutAsync(
                            ReferenceList.StaffUpdateUser,
                            new StringContent(myJson, Encoding.UTF8, "application/json"));

                json = await post.Content.ReadAsStringAsync();
            }

            try
            {
                json = JsonMapper.ToObject(json.ToString());
                response.Response = (int)json["response"];
                response.Status = json["status"].ToString();
                response.Info = json["info"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
            }

            return response;
        }

        public async Task<AuthResponseModel> DeleteUser(int id)
        {
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.DeleteAsync(
                            ReferenceList.StaffDeleteUser +
                            $"{ReferenceList.Token}/{id}");

                json = await post.Content.ReadAsStringAsync();
            }

            try
            {
                json = JsonMapper.ToObject(json.ToString());
                response.Response = (int)json["response"];
                response.Status = json["status"].ToString();
                response.Info = json["info"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
            }

            return response;
        }
    }
}
