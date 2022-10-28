namespace Middle
{
    public class MyMiddle
    {
        private readonly RequestDelegate _delegate;

        public MyMiddle(RequestDelegate @delegate)
        {
            _delegate = @delegate;
        }
        public async Task InvokeAsync(HttpContext http)
        {
            http.Response.WriteAsync("MyMiddleWare start</br>");
            await _delegate.Invoke(http);
            http.Response.WriteAsync("MyMiddleWare end</br>");
        }
    }
}
