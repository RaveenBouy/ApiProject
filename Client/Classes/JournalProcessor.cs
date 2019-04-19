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
    public class JournalProcessor
    {
        public List<ItemModel> GetJournals(string token, string type, string value)
        {
            var journalList = new List<ItemModel>();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.JournalMember +
                $"/{token}/{type}/{value}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        journalList.Add(new ItemModel
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

                    return journalList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<ItemModel> GetAllJournals(string token)
        {
            var journalList = new List<ItemModel>();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.JournalMember +
                $"/{token}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        journalList.Add(new ItemModel
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

                    return journalList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<AuthResponseModel> InsertJournal(string title, string description, string author, int publishYear, string access)
        {
            string myJson = "{'title': '" + title + "', 'description' : '" + description + "', 'author':'" + author + "', 'publishYear' : " + publishYear + ", 'category' : 'journal', 'access' : '" + access + "', 'authToken' : '" + ReferenceList.Token + "'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PostAsync(
                            ReferenceList.JournalStaff,
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

        public async Task<AuthResponseModel> UpdateJournal(int id, string type, string value)
        {
            string myJson = "{'id': '" + id + "', 'type' : '" + type + "', 'value':'" + value + "', 'authToken' : '" + ReferenceList.Token + "'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PutAsync(
                            ReferenceList.JournalStaff,
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

        public async Task<AuthResponseModel> DeleteJournal(int id)
        {
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.DeleteAsync(
                            ReferenceList.JournalStaff +
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
