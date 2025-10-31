using Api.Helpers;
using Domain.Entities;
using Domain.Filters; 
using Microsoft.AspNetCore.Mvc;
using Service.Business;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private AlunoService _service;
        public AlunoController(AlunoService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("GetData")]
        public async Task<IActionResult> Get([FromQuery] AlunoFilter filters)
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
        public async Task<IActionResult> GetDataItem([FromQuery] AlunoFilter filters)
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
        public async Task<IActionResult> GetById([FromQuery] AlunoFilter filters)
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
        public async Task<IActionResult> Save([FromBody] Aluno model)
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
        public async Task<IActionResult> SavePartial([FromBody] Aluno model)
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
        public async Task<IActionResult> Remove([FromBody] Aluno model)
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
