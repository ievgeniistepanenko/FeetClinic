using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using BE.Interfaces;


namespace FeetClinic.WEB.ServiceGateway
{
    public class ServiceGateway<TEntity> where TEntity : IEntity
    {

        protected readonly string path;
        protected string hostUri;

        public ServiceGateway(string path, string hostUri = "")
        {
            this.hostUri = hostUri;
            this.path = path;
        }

        public IEnumerable<TEntity> GetAll()
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;
            IEnumerable<TEntity> entities;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                entities = new List<TEntity>();
            }
            else
            {
                entities = response.Content.ReadAsAsync<IEnumerable<TEntity>>().Result;
            }
            
            return entities;

        }
        public IEnumerable<TEntity> GetAll(string properties)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + "?properties=" + properties).Result;
            IEnumerable<TEntity> entities;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                entities = new List<TEntity>();
            }
            else
            {
                entities = response.Content.ReadAsAsync<IEnumerable<TEntity>>().Result;
            }

            return entities;

        }

        public TEntity GetOne(int id)
        {
            TEntity entity;
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + id.ToString()).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                entity = response.Content.ReadAsAsync<TEntity>().Result;
            }
            else
            {
                entity = default(TEntity);
            }
            
            return entity;
        }

        public TEntity GetOne(int id, string properties)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.GetAsync(path + id.ToString() + "?properties=" + properties).Result;
            var entity = response.Content.ReadAsAsync<TEntity>().Result;
            return entity;
        }

        public HttpResponseMessage CreateOne(TEntity entity)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.PostAsJsonAsync(path, entity).Result;
            return response;
        }

        public HttpResponseMessage Update(TEntity entity)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.PutAsJsonAsync(path + entity.Id.ToString(), entity).Result;
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            HttpClient client = GetHttpClient();
            HttpResponseMessage response = client.DeleteAsync(path + id.ToString()).Result;

            return response;
        }

        protected HttpClient GetHttpClient()
        {
            string baseAddress =
                WebConfigurationManager.AppSettings["FeetClinicRestApiBaseAddress"];
            if (hostUri == "")
            {
                hostUri = baseAddress;
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(hostUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

       
    }
}
