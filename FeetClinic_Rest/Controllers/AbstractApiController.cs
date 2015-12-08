using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BE.Interfaces;
using BLL;
using BLL.Managers;
using BE.BE;
using FeetClinic_DAL.Abstarct;
using FeetClinic_DAL.Conrete;

namespace FeetClinic_Rest.Controllers
{
    public abstract class AbstractApiController<TEntity,TManager> : ApiController 
        where TEntity: class, IEntity 
        where TManager : AbstractManager<TEntity>
    {

        protected BllFacade BllFacade;
        protected TManager Manager; 

        protected AbstractApiController()
        {
            BllFacade = new BllFacade();
            
        }

        protected abstract TManager GetManager();

        protected virtual HttpResponseMessage GetAll()
        {
            return GetAll("");

        }
        protected virtual HttpResponseMessage GetAll(string properties)
        {
            try
            {
                List<TEntity> entities = Manager.GetAll(properties).ToList(); 
                if (entities.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        protected virtual HttpResponseMessage GetOne(int id)
        {
            return GetOne(id, "");
        }
        protected virtual HttpResponseMessage GetOne(int id,string properties)
        {
            try
            {
                TEntity entity = Manager.GetOne(id,properties);
                if (entity == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        protected virtual HttpResponseMessage UpdateOne(int id, TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (id != entity.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Entities id is not the same with given id");
            }

            try
            {
                Manager.Update(entity);
            }
            catch (Exception ex)
            {
                if (!IsExists(id))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Entity with given id not found");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }
        protected virtual HttpResponseMessage CreateOne(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                entity = Manager.Create(entity);
                var response = Request.CreateResponse(HttpStatusCode.Created, entity);

                string uri = Url.Link("DefaultApi", new { id = entity.Id });
                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        protected virtual HttpResponseMessage DeleteOne(int id)
        {
            try
            {
                TEntity entity =  Manager.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        protected override void Dispose(bool disposing)
        {
            Manager.Dispose();
            base.Dispose(disposing);
        }
        protected bool IsExists(int id)
        {
            return Manager.Any(e => e.Id == id);
        }

    }
}
