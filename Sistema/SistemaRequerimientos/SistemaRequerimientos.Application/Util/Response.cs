using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRequerimientos.Application.Util
{
    public class Response<TResponse>
    {
        public HttpStatusCode Status { get; }
        public bool EsError { get; }
        public TResponse Result { get; set; }
        public object Mensaje { get; set; }
        public Response(TResponse response, object mensaje)
        {
            Result = response;
            EsError = false;
            Mensaje = mensaje;
            Status = HttpStatusCode.OK;
        }
        protected Response(Exception errors)
        {
            Mensaje = errors.Message;
            EsError = true;
            Status = HttpStatusCode.InternalServerError;
        }

        protected Response(object mensajeControlado)
        {
            Mensaje = mensajeControlado;
            EsError = false;
            Status = HttpStatusCode.OK;
        }

        public static Response<TResponse> Error(Exception error)
        {
            return new Response<TResponse>(error);
        }

        public static Response<TResponse> Ok(TResponse response, object mensaje)
        {
            return new Response<TResponse>(response, mensaje);
        }

        public static Response<TResponse> Warning(object mensajeControlado)
        {
            return new Response<TResponse>(mensajeControlado);
        }
    }
}
