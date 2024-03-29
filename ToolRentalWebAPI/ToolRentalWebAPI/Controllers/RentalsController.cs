﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ToolRentalWebAPI.Models;

namespace ToolRentalWebAPI.Controllers
{
    public class RentalsController : ApiController
    {
		private YourProjectToolRentalEntities2 db = new YourProjectToolRentalEntities2();

		// GET: api/Rentals
		public IQueryable<Rental> GetRentals()
		{
			return db.Rentals;
		}

		// GET: api/Rentals/5
		[ResponseType(typeof(Rental))]
		public IHttpActionResult GetRental(int id)
		{
			Rental rental = db.Rentals.Find(id);
			if (rental == null)
			{
				return NotFound();
			}

			return Ok(rental);
		}

		// PUT: api/Rentals/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRental(int id, Rental rental)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != rental.RentalId)
			{
				return BadRequest();
			}

			db.Entry(rental).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RentalExists(id))
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

		// POST: api/Rentals
		[ResponseType(typeof(Rental))]
		public IHttpActionResult PostRental(Rental rental)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Rentals.Add(rental);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = rental.RentalId }, rental);
		}

		// DELETE: api/Rentals/5
		[ResponseType(typeof(Rental))]
		public IHttpActionResult DeleteRental(int id)
		{
			Rental rental = db.Rentals.Find(id);
			if (rental == null)
			{
				return NotFound();
			}

			db.Rentals.Remove(rental);
			db.SaveChanges();

			return Ok(rental);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool RentalExists(int id)
		{
			return db.Rentals.Count(e => e.RentalId == id) > 0;
		}

		// GET: api/GetMaxId/
		[HttpGet]
		[Route("api/GetRentalMaxId")]
		public IHttpActionResult GetRentalMaxId()
		{
			// to get Rental Items using RentalId
			if (db.Rentals.Count() > 0)
			{
				return Ok(db.Rentals.Max(r => r.RentalId) + 1);
			}
			else
			{
				return Ok(1);
			}
		}
	}
}
