using Data.Repositories;

namespace AppyPrjSaasPortalAPI.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly Repository Repository;

        protected IActionResult HandleExceptionResponse(Exception ex)
        {
            Log.Error(ex, "Exception occured - {0}", ex);
            if (ex.InnerException != null) Log.Error(ex.InnerException, "Exception occured - {0}", ex.InnerException.Message);
            return BadRequest(ex);
        }

        protected IActionResult EntiyCreatedResponse(string entity)
        {
            return Ok(new GenericAPIResponse(string.Format(Messages.ApiResponse_created, entity)));
        }
        protected IActionResult EntiyUpdatedesponse(string entity)
        {
            return Ok(new GenericAPIResponse(string.Format(Messages.ApiResponse_updated, entity)));
        }
        protected IActionResult EntityGetResponse(object data)
        {
            return Ok(new GenericAPIResponse(Messages.ApiResponse_success, data));
        }    
    }

    internal class GenericAPIResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public GenericAPIResponse(string message, object data = null)
        {
            Message = message;
            Data = data ?? Array.Empty<string>();
        }
    }
}
