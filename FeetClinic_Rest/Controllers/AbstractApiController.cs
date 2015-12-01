﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainModel.BE;
using DomainModel.BLL.Interfaces;
using FeetClinic_DAL.Abstarct;
using FeetClinic_DAL.Conrete;

namespace FeetClinic_Rest.Controllers
{
    public abstract class AbstractApiController<TEntity> : ApiController where TEntity: class, IEntity
    {

        protected DalFacade Facade;
        protected IRepository<TEntity> Repository;
        protected AbstractApiController()
        {
            Facade = new DalFacade();
        }

        protected virtual HttpResponseMessage GetAll()
        {
            try
            {
                IEnumerable<TEntity> entities = Repository.GetAll();
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
            try
            {
                TEntity entity = Repository.GetOne(a => a.Id == id);
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
                Repository.Update(entity);
                Facade.Save();
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
                entity = Repository.Create(entity);
                Facade.Save();
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
                TEntity entity = Repository.Delete(id);
                Facade.Save();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        protected override void Dispose(bool disposing)
        {
            Facade.Dispose();
            base.Dispose(disposing);
        }

        protected bool IsExists(int id)
        {
            return Repository.Any(c => c.Id == id);
        }

    }
}