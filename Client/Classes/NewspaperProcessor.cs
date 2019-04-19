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

namespace Client.Classes
{
    public class NewspaperProcessor
    {
        public List<ItemModel> GetNewspapers(string token, string type, string value)
        {
            var newspaperList = new List<ItemModel>();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.NewspaperMember +
                $"/{token}/{type}/{value}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        newspaperList.Add(new ItemModel
                        {
                            Id = (int)json[i]["id"],
                            Title = json[i]["title"].ToString(),
                            Description = json[i]["description"].ToString(),
                            Author = json[i]["author"].ToString(),
                            PublishYear = (int)json[i]["publishYear"],
                            Access = json[i]["access"].ToString(),
                            DateAdded = json[i]["dateAdded"].ToString()
                        });
                    }

                    return newspaperList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<ItemModel> GetAllNewspapers(string token)
        {
            var newspaperList = new List<ItemModel>();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.NewspaperMember +
                $"/{token}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        newspaperList.Add(new ItemModel
                        {
                            Id = (int)json[i]["id"],
                            Title = json[i]["title"].ToString(),
                            Description = json[i]["description"].ToString(),
                            Author = json[i]["author"].ToString(),
                            PublishYear = (int)json[i]["publishYear"],
                            Access = json[i]["access"].ToString(),
                            DateAdded = json[i]["dateAdded"].ToString()
                        });
                    }

                    return newspaperList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<AuthResponseModel> InsertNewspaper(string title, string description, string author, int publishYear, string access)
        {
            string myJson = "{'title': '" + title + "', 'description' : '" + description + "', 'author':'" + author + "', 'publishYear' : " + publishYear + ", 'category' : 'newspaper', 'access' : '" + access + "', 'authToken' : '" + ReferenceList.Token + "'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PostAsync(
                            ReferenceList.NewspaperStaff,
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

        public async Task<AuthResponseModel> UpdateNewspaper(int id, string type, string value)
        {
            string myJson = "{'id': '" + id + "', 'type' : '" + type + "', 'value':'" + value + "', 'authToken' : '" + ReferenceList.Token + "'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PutAsync(
                            ReferenceList.NewspaperStaff,
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

        public async Task<AuthResponseModel> DeleteNewspaper(int id)
        {
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.DeleteAsync(
                            ReferenceList.NewspaperStaff +
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
