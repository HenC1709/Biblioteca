namespace BibliotecaV2.Application.DTOs
{
    public class OperationResult<T>
    {
        public bool Exitoso { get; }
        public string Mensaje { get; }
        public T? Dato { get; }

        private OperationResult(bool exitoso, string mensaje, T? dato)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
            Dato = dato;
        }

        public static OperationResult<T> Ok(string mensaje, T? dato = default)
            => new(true, mensaje, dato);

        public static OperationResult<T> Error(string mensaje)
            => new(false, mensaje, default);
    }
}