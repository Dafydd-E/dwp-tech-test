using Location.Domain;
using Location.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace NearLondonLocationAPI.Location
{
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private static readonly Coordinate LondonCoordinate = new Coordinate(51.5074, 0.1278);

        private readonly ICommandService<UsersWithinDistanceCommand, UsersWithinDistanceCommandResult> userWithinDistanceCommmandHandler;

        public LocationController(
            ICommandService<UsersWithinDistanceCommand, UsersWithinDistanceCommandResult> userWithinDistanceCommmandHandler)
        {
            this.userWithinDistanceCommmandHandler = userWithinDistanceCommmandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersWithinDistanceOfCity(string city, double withinDistance)
        {
            if (city?.ToLower() != "london")
            {
                return this.StatusCode(
                    (int)HttpStatusCode.UnprocessableEntity,
                    new
                    {
                        Reason = "Only London City is currently supported",
                    });
            }

            var result = await this.userWithinDistanceCommmandHandler.Execute(new UsersWithinDistanceCommand(city, withinDistance, LondonCoordinate));

            if (!result.IsSuccess)
            {
                return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Exception);
            }

            return this.Ok(result.Users);
        }
    }
}
