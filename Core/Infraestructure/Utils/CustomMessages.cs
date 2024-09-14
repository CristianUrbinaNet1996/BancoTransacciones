namespace BCuentas.Utils
{
    public class CustomMessages
    {
        public static class ErrorMessages
        {
            public const string FalloAlguardarEnDb = "Error al insertar el registro del tipo {0} en la base de datos";

            public const string TarjetaNoEncontrada = "Tarjeta de credito no encontrada con Id {0}";

            public const string FondosInsuficientes = "Compra rechazada, fondos insuficientes en la tarjeta {0}";

            public const string CredencialesIncorrectas = "Email o contraseña incorrecta";
        }
    }
}
