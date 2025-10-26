using Api.Helpers;
using Domain.Entities;
using Domain.Filters; 
using Microsoft.AspNetCore.Mvc;
using Service.Business;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class #Entity#Controller : ControllerBase
    {
        private #Entity#Service _service;
        public #Entity#Controller(#Entity#Service service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("GetData")]
        public async Task<dynamic> Get([FromQuery] #Entity#Filter filters)
        {
            HttpResult result = new HttpResult();
            try
            {
                var searchResult = await _service.GetData(filters);
                return result.ReturnCustomResponse(searchResult);  
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex);
                return responseEx;
            } 
        }

        [HttpGet]
        [Route("GetDataItem")]
        public async Task<dynamic> GetDataItem([FromQuery] #Entity#Filter filters)
        {
            HttpResult result = new HttpResult();
            try
            {
                var searchResult = await _service.GetDataItem(filters);
                return result.ReturnCustomResponse(searchResult);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex);
                return responseEx;
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<dynamic> GetById([FromQuery] #Entity#Filter filters)
        {
            HttpResult result = new HttpResult();
            try
            {
                var searchResult = await _service.GetOne(filters);
                return result.ReturnCustomResponse(searchResult);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex);
                return responseEx;
            } 
        }

        [HttpPost] 
        public async Task<dynamic> Save([FromBody] #Entity# model)
        {
            HttpResult result = new HttpResult();
            try
            {
                List<string> erros = new List<string>();
                if (_service.Validate(model, out erros))
                {
                    var newModel = await _service.Save(model);
                    return result.ReturnCustomResponse(newModel);
                }
                else
                {
                    return result.ReturnCustomErrors(erros);
                }
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex);
                return responseEx;
            }
        }

        [HttpPut] 
        public async Task<dynamic> SavePartial([FromBody] #Entity# model)
        {
            HttpResult result = new HttpResult();
            try
            {
                List<string> erros = new List<string>();
                if (_service.Validate(model, out erros))
                {
                    var newModel = await _service.SavePartial(model);
                    return result.ReturnCustomResponse(newModel);
                }
                else
                {
                    return result.ReturnCustomErrors(erros);
                }
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex);
                return responseEx;
            }
        }

        [HttpDelete] 
        public async Task<dynamic> Remove([FromBody] #Entity# model)
        {
            HttpResult result = new HttpResult();
            try
            {
                await _service.Remove(model);
                return result.ReturnCustomResponse("");
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex);
                return responseEx;
            }
        }
    }
}
