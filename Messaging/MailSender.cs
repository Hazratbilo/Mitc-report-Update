using brevo_csharp.Api;
using brevo_csharp.Model;
using MITCRMS.Extensions;
using MITCRMS.Interface.Messaging;

namespace MITCRMS.Implementation.Messaging
{
    public class MailSender(IConfiguration config, IWebHostEnvironment env,
        ILogger<MailSender> logger) : IMailSender
    {
        private readonly IConfiguration _config = config ?? throw new ArgumentNullException(nameof(config));
        private readonly IWebHostEnvironment _env = env ?? throw new ArgumentNullException(nameof(env));
        private readonly ILogger<MailSender> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public Task<bool> SendEmailAsync(string from, string fromName, string to, string toName, string subject, string body, IDictionary<string, Stream> attachments = null)
        {
            var smtpApiKey = _config["MITCRMSSettings:SmtpApiKey"];

            var apiInstance = new TransactionalEmailsApi();
            var sendSmtpEmail = new SendSmtpEmail
            {
                Sender = new SendSmtpEmailSender { Email = from, Name = fromName },
                To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo { Email = to, Name = toName } },
                Subject = subject,
                HtmlContent = body,
                Attachment = new List<SendSmtpEmailAttachment>()

            };

            foreach(var asset in EmailAssetRegistry.AssetMap)
            {
                if(body.Contains($"cid:{asset.Key}"))
                {
                    string absolutePath = Path.Combine(_env.WebRootPath, asset.Value);
                    var stream = new MemoryStream(File.ReadAllBytes(Path.Combine(_env.ContentRootPath, "EmailAssets", asset.Value))));
                    
                    if(File.Exists(absolutePath))
                    {
                        byte[] imageBytes = File.ReadAllBytes(absolutePath);
                        sendSmtpEmail.Attachment.Add(new SendSmtpEmailAttachment(
                            name: asset.Key,
                            content: imageBytes
                        ));

                    }
                    else
                    {
                        _logger.LogWarning("Email asset not found: {AssetPath}", absolutePath);
                    }
                }
            }

            if(attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    sendSmtpEmail.Attachment.Add(new SendSmtpEmailAttachment
                    (content: ReadFully(attachment.Value), name: attachment.Key);
                        
                }
            }

            if (!string.IsNullOrEmpty(smtpApiKey))
            {
                brevo_csharp.Client.Configuration.Default.AddApiKey("api-key", smtpApiKey);
                try
                {
                    await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError("Exception when calling TransactionalEmailsApi.SendTransacEmail: " + e.Message);
                    throw new MailSenderException(e.Message, e);
                }
            }

            _logger.LogError("SMTP API Key is not configured.");
            throw new MailSenderException("SMTP API Key is not configured.");

        }

        private static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
