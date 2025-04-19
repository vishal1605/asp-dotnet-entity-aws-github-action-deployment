using AshidaERPNext.Models.Ajax;
using entity_framework_aws_deployment.Models;
using entity_framework_aws_deployment.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace entity_framework_aws_deployment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserDetailsDbContext _context;

        public HomeController(UserDetailsDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserdetailsAJAXGET(string requestType, string requestData)
        {

            ServerResponse serverResponse = new();

            try
            {
                switch (requestType)
                {
                    case "GetAllUserDetails":
                        {
                            serverResponse.Response = new
                            {
                                data = _context.UserDetails.ToList()
                            };

                            break;
                        }

                    default:
                        {
                            throw new InvalidOperationException("Invalid 'requestType'.");
                        }
                }

                return new JsonResult(serverResponse);
            }
            catch (Exception ex)
            {
                serverResponse.Success = false;
                serverResponse.Message.Type = "Error";
                serverResponse.Message.Value = ex.Message;

                serverResponse.Response = new
                {
                    ex.StackTrace,
                    ex.InnerException
                };

                return new JsonResult(serverResponse);
            }
        }

        [HttpPost]
        public IActionResult UserdetailsAJAXPOST(string requestType, string requestData)
        {
            try
            {
                ServerResponseDatatable serverResponseDatatable = new();
                switch (requestType)
                {
                    case "UserdetailsProcess":
                        {
                            dynamic? requestDataJSON = JsonConvert.DeserializeObject(requestData);
                            if (requestDataJSON is null)
                                throw new InvalidOperationException("Invalid 'requestData'.");

                            switch (requestDataJSON["action"].ToString())
                            {
                                case "create":
                                    {
                                        var data = new List<dynamic>();
                                        foreach (var property in requestDataJSON["data"].Properties())
                                        {
                                            UserDetails? user = new UserDetails();
                                            user.UserName = property.Value["userName"].ToString();
                                            user.Password = property.Value["password"].ToString();
                                            user.Gender = property.Value["gender"].ToString();
                                            user.Age = property.Value["age"].ToString();
                                            user.CreatedDate = DateTime.Now.ToString("dd-MM-yyyy");

                                            _context.UserDetails.Add(user);
                                            _context.SaveChanges();
                                            data.Add(user);
                                        }
                                        serverResponseDatatable.Data = data;

                                        break;
                                    }

                                case "edit":
                                    {
                                        var data = new List<dynamic>();

                                        foreach (var property in requestDataJSON["data"].Properties())
                                        {
                                            int id = int.Parse(property.Name.ToString());
                                            UserDetails? user = _context.UserDetails.FirstOrDefault(u => u.Id == id);
                                            if (user != null)
                                            {
                                                user.UserName = property.Value["userName"].ToString();
                                                user.Password = property.Value["password"].ToString();
                                                user.Gender = property.Value["gender"].ToString();
                                                user.Age = property.Value["age"].ToString();
                                                user.UpdatedDate = DateTime.Now.ToString("dd-MM-yyyy");
                                                _context.SaveChanges();

                                            }

                                            data.Add(user);
                                        }
                                        serverResponseDatatable.Data = data;

                                        break;
                                    }

                                case "remove":
                                    {
                                        foreach (var property in requestDataJSON["data"].Properties())
                                        {
                                            int id = int.Parse(property.Name.ToString());
                                            UserDetails? user = _context.UserDetails.FirstOrDefault(u => u.Id == id);
                                            if (user != null)
                                            {
                                                _context.UserDetails.Remove(user);
                                                _context.SaveChanges();
                                            }

                                        }

                                        break;
                                    }
                                default:
                                    {
                                        throw new InvalidOperationException("Invalid 'requestData.action'.");

                                    }
                            }
                            break;
                        }

                    case "InsertData":
                        {
                            JObject requestDataJObject = JObject.Parse(requestData);
                            HashSet<string> expectedKeys = new HashSet<string> { "userName", "password", "gender", "age" };
                            List<string> actualKeys = requestDataJObject.Properties().Select(p => p.Name).ToList();
                            List<string> unexpectedKeys = actualKeys.Where(key => !expectedKeys.Contains(key)).ToList();
                            List<string> missingKeys = expectedKeys.Where(key => !actualKeys.Contains(key)).ToList();

                            if (unexpectedKeys.Any() || missingKeys.Any())
                            {
                                List<string> errorMessages = new List<string>();
                                if (unexpectedKeys.Any())
                                {
                                    errorMessages.Add($"Unexpected key(s): {string.Join(", ", unexpectedKeys)}");
                                }
                                if (missingKeys.Any())
                                {
                                    errorMessages.Add($"Missing required key(s): {string.Join(", ", missingKeys)}");
                                }
                                errorMessages.Add($"Expected keys are: {string.Join(", ", expectedKeys)}");

                                throw new InvalidOperationException($"Invalid 'requestData'. {string.Join(". ", errorMessages)}");
                            }

                            dynamic? requestDataJSON = JsonConvert.DeserializeAnonymousType(requestData, new
                            {
                                userName = "",
                                password = "",
                                gender = "",
                                age = ""
                            });
                            if (requestDataJSON is null)
                                throw new InvalidOperationException("Invalid 'requestData'.");

                            var data = new List<dynamic>();

                            UserDetails? user = new UserDetails();
                            user.UserName = requestDataJSON.userName;
                            user.Password = requestDataJSON.password;
                            user.Gender = requestDataJSON.gender;
                            user.Age = requestDataJSON.age;
                            user.CreatedDate = DateTime.Now.ToString("dd-MM-yyyy");

                            _context.UserDetails.Add(user);
                            _context.SaveChanges();
                            data.Add(user);
                            serverResponseDatatable.Data = data;

                            break;
                        }

                    default:
                        {
                            throw new InvalidOperationException("Invalid 'requestType'.");
                        }
                }
                return new JsonResult(serverResponseDatatable);
            }
            catch (Exception ex)
            {
                ServerResponseDatatable serverResponseDatatable = new()
                {
                    Error = ex.Message
                };

                return new JsonResult(serverResponseDatatable);
            }
        }

    }
}
