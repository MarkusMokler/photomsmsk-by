using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using PhotoMSK.Data;
using PhotoMSK.Models;
using PhotoMSK.ViewModels;


namespace PhotoMSK.Extensiolns
{
    public static class ApiControllerExtension
    {
        public static bool IsNotValidShortcut(this ApiController controller, AppContext context, string shortcut, out IHttpActionResult result)
        {
            if (string.IsNullOrEmpty(shortcut))
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { "Shortcut" }, Message = "короткая ссылка не может содержать пробел" }, controller);
                return true;
            }

            if (shortcut.Contains(" "))
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { "Shortcut" }, Message = "короткая ссылка не может содержать пробел" }, controller);
                return true;
            }

            if (shortcut.Contains(" "))
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { "Shortcut" }, Message = "короткая ссылка не может содержать пробел" }, controller);
                return true;
            }

            if (shortcut.Contains("."))
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { "Shortcut" }, Message = "короткая ссылка не может содержать '.'" }, controller);
                return true;
            }

            if (context.Routes.Any(x => x.Shortcut == shortcut))
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { "Shortcut" }, Message = "Такая короткая ссылка уже имеется", }, controller);
                return true;
            }
            result = null;
            return false;
        }

        public static bool IsNameNotValid(this ApiController controller,out IHttpActionResult result, string name,string fieldName )
        {
            if (string.IsNullOrEmpty(name))
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { fieldName }, Message = "Имя не может быть пустым" }, controller);
                return true;
            }
            if (name.Length < 3)
            {
                result = new OkNegotiatedContentResult<object>(new ErrorViewModel { Error = 1, ErrorFields = new[] { fieldName }, Message = "Имя не может быть короче трех символов" }, controller);
                return true;
            }
            result = null;
            return false;
        }
    }
}