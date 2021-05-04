using apiGestores.Context;
using apiGestores.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGestores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GestoresController : Controller
    {
        private readonly AppDbContext context;

        public GestoresController(AppDbContext context )
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Gestores_DB.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID}", Name = "GetGestor")]
        public ActionResult Get(int ID)
        {
            try
            {
                var gestor = context.Gestores_DB.FirstOrDefault(g => g.ID == ID);
                return Ok(gestor);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /*[HttpPost]
        public ActionResult Post([FromBody]Gestores_Bd Gestor)
        {
            try
            {
                context.Gestores_DB.Add(Gestor);
                context.SaveChanges();
                return CreatedAtRoute("GetGestor", new { id = Gestor.ID }, Gestor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        [HttpPut("{ID}")]
        public ActionResult Put(int ID, [FromBody] Gestores_Bd Gestor)
        {
            try
            {
                if (Gestor.ID == ID)
                {
                    context.Entry(Gestor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetGestor", new { id = Gestor.ID }, Gestor);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        public ActionResult Delete(int ID)
        {
            try
            {
                var gestor = context.Gestores_DB.FirstOrDefault(g => g.ID == ID);
                if (gestor != null)
                {
                    context.Gestores_DB.Remove(gestor);
                    context.SaveChanges();
                    return Ok(ID);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
