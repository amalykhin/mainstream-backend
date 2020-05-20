using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteamingService.Entities;
using SteamingService.Models;
using SteamingService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SteamingService.Controllers
{
    [Route("api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStreamService _streamService;
        private readonly IMapper _mapper;

        public APIController(
            IUserService userService, IStreamService streamService,
            IMapper mapper)
        {
            _userService = userService;
            _streamService = streamService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]AuthenticateModel authInfo)
        {
            var (username, password) = authInfo;
            var user = _userService.Authenticate(username, password);

            if (user == null)
                return Unauthorized(new { message = "Couldn't authenticate using supplied credentials." });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity))
                .Wait();

            return Ok(new
            {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                state = user.State,
                streamerKey = user.StreamerKey
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterModel registrationInfo)
        {
            var newUser = _mapper.Map<User>(registrationInfo);
            try
            {
                _userService.Register(newUser, registrationInfo.Password);
                return Ok();
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("stream/viewers")]
        public IActionResult AddViewer([FromBody]ViewerModel addViewerInfo)
        {
            var (stream, viewer) = addViewerInfo;
            try
            {
                _streamService.AddViewer(stream, viewer);
                return Ok();
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("stream/viewers")]
        public IActionResult RemoveViewer([FromBody]ViewerModel removeViewerInfo)
        {
            var (stream, viewer) = removeViewerInfo;
            try
            {
                _streamService.RemoveViewer(stream, viewer);
                return Ok();
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("streams")]
        public IActionResult GetStreams()
        {
            return Ok(_streamService.GetStreams());
        }

        [HttpPost("streams")]
        public IActionResult StartStream([FromBody]StartStreamModel streamInfo)
        {
            try
            {
                var stream = new Stream
                {
                    Title = streamInfo.Title,
                    Description = streamInfo.Description,
                    BroadcastUri = streamInfo.StreamUri
                };
                stream = _streamService.StartStream(stream, streamInfo.Broadcaster);
                return Ok(stream);
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("streams/{broadcasterName}")]
        public IActionResult EndStream(string broadcasterName)
        {
            try
            {
                var stream = _streamService.GetStreams()
                    .Single(s => s.Broadcaster.Username == broadcasterName);
                _streamService.EndStream(stream);
                return Ok();
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("streams/{broadcasterName}")]
        public IActionResult GetStream(string broadcasterName)
        {
            try
            {
                var stream = _streamService.GetStreams()
                    .FirstOrDefault(s => s.Broadcaster.Username == broadcasterName);

                return Ok(stream);
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("user")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var username = this.User
                    .FindFirst(ClaimTypes.Name)
                    .Value;
                var user = _userService.GetUser(username);

                return Ok(new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    state = user.State,
                    streamerKey = user.StreamerKey
                });
            }
            catch (Exception e)
            {
                PrintException(e);
                return BadRequest(new { message = e.Message });
            }
        }

        private void PrintException(Exception e)
        {
            Console.Error.Write(e);
            Console.Error.Flush();
        }
    }
}
