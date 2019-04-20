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
    public class BookProcessor
    {
        public List<ItemModel> GetBooks(string token, string type, string value)
        {
            var bookList = new List<ItemModel>();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.BookMember +
                $"/{token}/{type}/{value}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        bookList.Add(new ItemModel
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
                    
                    return bookList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<ItemModel> GetAllBooks(string token)
        {
            var bookList = new List<ItemModel>();

            using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
            string.Concat(new string[]
            {
                ReferenceList.BookMember +
                $"/{token}"

            }))).GetResponse()).GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                try
                {
                    for (int i = 0; i < json.Count; i++)
                    {
                        bookList.Add(new ItemModel
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

                    return bookList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<AuthResponseModel> InsertBook(string title, string description, string author, int publishYear, string access)
        {
            string myJson = "{'title': '" + title + "', 'description' : '"+ description + "', 'author':'" + author + "', 'publishYear' : " + publishYear + ", 'category' : 'book', 'access' : '"+ access+"', 'authToken' : '"+ReferenceList.Token+"'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PostAsync(
                            ReferenceList.BookStaff,
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

        public async Task<AuthResponseModel> UpdateBook(int id, string type, string value)
        {
            string myJson = "{'id': '" + id + "', 'type' : '" + type + "', 'value':'" + value + "', 'authToken' : '" + ReferenceList.Token + "'}";
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.PutAsync(
                            ReferenceList.BookStaff,
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

        public async Task<AuthResponseModel> DeleteBook(int id)
        {
            JsonData json = new JsonData();
            var response = new AuthResponseModel();

            using (var client = new HttpClient())
            {
                var post = await client.DeleteAsync(
                            ReferenceList.BookStaff +
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

        public async Task<List<ItemModel>> GetVisitorBooks(string value, string searchvalue)
        {
            JsonData json = new JsonData();
            var bookList = new List<ItemModel>();

            using (var client = new HttpClient())
            {
                var get = await client.GetAsync(
                            ReferenceList.Book +
                            $"{value}/{searchvalue}");

                using (var reader = new StreamReader(await get.Content.ReadAsStreamAsync()))
                {
                    json = JsonMapper.ToObject(reader.ReadToEnd());
                }
            }

            try
            {
                for (int i = 0; i < json.Count; i++)
                {
                    bookList.Add(new ItemModel
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

                return bookList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ItemModel>> GetVisitorAllBooks()
        {
            JsonData json = new JsonData();
            var bookList = new List<ItemModel>();

            using (var client = new HttpClient())
            {
                var get = await client.GetAsync(
                            ReferenceList.Book);

                using (var reader = new StreamReader(await get.Content.ReadAsStreamAsync()))
                {
                    json = JsonMapper.ToObject(reader.ReadToEnd());
                }
            }

            try
            {
                for (int i = 0; i < json.Count; i++)
                {
                    bookList.Add(new ItemModel
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

                return bookList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
