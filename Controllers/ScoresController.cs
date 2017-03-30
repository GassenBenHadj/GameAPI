using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GameAPI.Models;

namespace GameAPI.Controllers
{
    public class ScoresController : ApiController
    {
        private GameAPIContext db = new GameAPIContext();

        // GET: api/Scores
        public IQueryable<Score> GetScores()
        {
            return db.Scores;
        }

        // GET: api/Scores/5
        [ResponseType(typeof(Score))]
        public async Task<IHttpActionResult> GetScore(int id)
        {
            Score score = await db.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            return Ok(score);
        }

        // PUT: api/Scores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScore(int id, Score score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != score.ScoreId)
            {
                return BadRequest();
            }

            db.Entry(score).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Scores
        [ResponseType(typeof(Score))]
        public async Task<IHttpActionResult> PostScore(Score score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scores.Add(score);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScoreExists(score.ScoreId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = score.ScoreId }, score);
        }

        // DELETE: api/Scores/5
        [ResponseType(typeof(Score))]
        public async Task<IHttpActionResult> DeleteScore(int id)
        {
            Score score = await db.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            db.Scores.Remove(score);
            await db.SaveChangesAsync();

            return Ok(score);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScoreExists(int id)
        {
            return db.Scores.Count(e => e.ScoreId == id) > 0;
        }
    }
}