using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender
{
    private readonly string fromEmail = "murilloivan735@gmail.com"; //remplaza por tu correo
    private readonly string fromPassword = "vkcp oajq iusd yaua"; // Reemplaza con la contraseña de aplicación generada para Gmail

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        try
        {
            using (var smtp = new SmtpClient("smtp.gmail.com", 587)) // Configuración de Gmail
            {
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true; // Habilita la conexión segura

                using (var mailMessage = new MailMessage(fromEmail, toEmail))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;

                    // Usa la versión asíncrona para enviar el correo
                    await smtp.SendMailAsync(mailMessage); // Enviar el correo  
                    Console.WriteLine("Correo enviado correctamente.");
                }
            }
        }
        catch (SmtpException smtpEx)
        {
            Console.WriteLine($"Error SMTP: {smtpEx.StatusCode} - {smtpEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error general: {ex.Message}");
            throw;
        }
    }

    public async Task SendRecoveryCodeAsync(string toEmail, string code)
    {
        string subject = "Código de recuperación de contraseña";
        string message = $"<h1>Recuperación de Contraseña</h1><p>Tu código de recuperación es: <strong>{code}</strong></p>";

        await SendEmailAsync(toEmail, subject, message);
    }
}