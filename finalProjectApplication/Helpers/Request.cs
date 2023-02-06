using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApplication
{
    public class Requests
    {
        public static IActionResult Response(ControllerBase Controller,
        ApiStatus statusCode, string msg, object dataValue)
        {
            var e = new ApiStatus(500);
            var _ = new
            {
                status = e.StatusCode,
                error = true,
                detail = "",
                message = e.StatusDescription,
                data = dataValue
            };

            if (statusCode.StatusCode != 200)
            {
                _ = new
                {
                    status = statusCode.StatusCode,
                    error = true,
                    detail = msg,
                    message = statusCode.StatusDescription,
                    data = dataValue
                };
            }
            else
            {
                _ = new
                {
                    status = statusCode.StatusCode,
                    error = false,
                    detail = msg,
                    message = statusCode.StatusDescription,
                    data = dataValue
                };
            }

            return Controller.StatusCode(statusCode.StatusCode, _);
        }
    }
}