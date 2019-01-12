using GBM.Challenge.Api.Models;
using GBM.Challenge.Domain;
using GBM.Challenge.Domain.Contracts;
using GBM.Challenge.Domain.Models;
using GBM.Challenge.Logic;
using GBM.Challenge.Tools;
using GBM.Challenge.Tools.Exception;
using GBM.Challenge.Tools.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GBM.Challenge.Api.Controllers
{
    [RoutePrefix("api/geolocation")]
    public class GeolocationController : ApiController
    {
        [HttpGet]
        [Route("date")]
        public async Task<string> GetServerDate()
        {
            return await Task.FromResult(DateTime.Now.ToString());
        }

        [HttpPost]
        [Route("coordinates/add")]
        public async Task<IHttpActionResult> PostPosition(HttpRequestMessage request)
        {
            Stream streamContent = await request.Content.ReadAsStreamAsync();
            streamContent.Seek(0, SeekOrigin.Begin);

            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                GeographicalPosition position;
                using (StreamReader requestReader = new StreamReader(streamContent, Encoding.GetEncoding("ISO-8859-1")))
                {
                    position = JsonConvert.DeserializeObject<GeographicalPosition>(requestReader.ReadToEnd());
                    position.RegistrationDate = Kit.GetDateTime();
                    position.Id = Guid.NewGuid();
                }

                using (LogicEngine logicEngine = new LogicEngine(DataEngineKind.Storage))
                {
                    await logicEngine.SetCurrentPosition(position);
                }

                using (LogicEngine logicEngine = new LogicEngine(DataEngineKind.Sql))
                {
                    await logicEngine.SetPositionHistory(position);
                }
            }
            catch (PlatformException ex)
            {
                response.Code = -1;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }
            catch (LogicException ex)
            {
                response.Code = 100;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }
            catch (Exception ex)
            {
                response.Code = -200;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }

            return Json(response, new JsonSerializerSettings());
        }

        [HttpPost]
        [Route("coordinates/current")]
        public async Task<IHttpActionResult> GetCurrentPosition(HttpRequestMessage request)
        {
            Stream streamContent = await request.Content.ReadAsStreamAsync();
            streamContent.Seek(0, SeekOrigin.Begin);

            ApiResponse<IGeographicalPosition> response = new ApiResponse<IGeographicalPosition>();

            try
            {
                RequestGetPositionModel requestModel;
                using (StreamReader requestReader = new StreamReader(streamContent, Encoding.GetEncoding("ISO-8859-1")))
                {
                    requestModel = JsonConvert.DeserializeObject<RequestGetPositionModel>(requestReader.ReadToEnd());
                }

                using (LogicEngine logicEngine = new LogicEngine(DataEngineKind.Storage))
                {
                    response.Result = await logicEngine.GetCurrentPosition(requestModel.TaxyId, requestModel.TravelId);
                }
            }
            catch (PlatformException ex)
            {
                response.Code = -1;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }
            catch (LogicException ex)
            {
                response.Code = 100;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }
            catch (Exception ex)
            {
                response.Code = -200;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }

            return Json(response, new JsonSerializerSettings());
        }

        [HttpPost]
        [Route("coordinates/history")]
        public async Task<IHttpActionResult> GetPositionsByTravelHistory(HttpRequestMessage request)
        {
            Stream streamContent = await request.Content.ReadAsStreamAsync();
            streamContent.Seek(0, SeekOrigin.Begin);

            ApiResponse<ICollection<IGeographicalPosition>> response = new ApiResponse<ICollection<IGeographicalPosition>>();

            try
            {
                RequestGetPositionModel requestModel;
                using (StreamReader requestReader = new StreamReader(streamContent, Encoding.GetEncoding("ISO-8859-1")))
                {
                    requestModel = JsonConvert.DeserializeObject<RequestGetPositionModel>(requestReader.ReadToEnd());
                }

                using (LogicEngine logicEngine = new LogicEngine(DataEngineKind.Sql))
                {
                    response.Result = await logicEngine.GetPositionsByTravelHistory(requestModel.TaxyId, requestModel.TravelId);
                }
            }
            catch (PlatformException ex)
            {
                response.Code = -1;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }
            catch (LogicException ex)
            {
                response.Code = 100;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }
            catch (Exception ex)
            {
                response.Code = -200;
                response.Message = ex.Message;
                EventManager.WriteEvent(ex);
            }

            return Json(response, new JsonSerializerSettings());
        }
    }
}
