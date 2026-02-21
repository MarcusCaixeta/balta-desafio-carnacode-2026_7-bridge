// DESAFIO: Sistema de Notificações Multi-Plataforma

using System;

namespace DesignPatternChallenge
{
    public interface INotificationRenderer
    {
        void RenderText(string title, string content);
        void RenderImage(string title, string content, string mediaUrl);
        void RenderVideo(string title, string content, string mediaUrl);
    }

    public class WebRenderer : INotificationRenderer
    {
        public void RenderText(string title, string content)
        {
            Console.WriteLine("[Web - HTML] <div class='notification'>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }

        public void RenderImage(string title, string content, string imageUrl)
        {
            Console.WriteLine("[Web - HTML] <div class='notification-image'>");
            Console.WriteLine($"  <img src='{imageUrl}' />");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }

        public void RenderVideo(string title, string content, string videoUrl)
        {
            Console.WriteLine("[Web - HTML] <div class='notification-video'>");
            Console.WriteLine($"  <video src='{videoUrl}' controls></video>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }
    }

    public class MobileRenderer : INotificationRenderer
    {
        public void RenderText(string title, string content)
        {
            Console.WriteLine("[Mobile - Native] Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine("Icon: notification_icon.png");
        }

        public void RenderImage(string title, string content, string imageUrl)
        {
            Console.WriteLine("[Mobile - Native] Rich Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Image: {imageUrl}");
            Console.WriteLine("Style: BigPictureStyle");
        }

        public void RenderVideo(string title, string content, string videoUrl)
        {
            Console.WriteLine("[Mobile - Native] Video Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Video: {videoUrl}");
            Console.WriteLine("Action: Tap to play");
        }
    }

    public class DesktopRenderer : INotificationRenderer
    {
        public void RenderText(string title, string content)
        {
            Console.WriteLine("[Desktop - Toast] Windows Notification:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine("╚══════════════════════════╝");
        }

        public void RenderImage(string title, string content, string imageUrl)
        {
            Console.WriteLine("[Desktop - Toast] Windows Notification with Image:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ [IMG: {imageUrl.Substring(0, Math.Min(15, imageUrl.Length))}...]  ║");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine("╚══════════════════════════╝");
        }

        public void RenderVideo(string title, string content, string videoUrl)
        {
            Console.WriteLine("[Desktop - Toast] Windows Notification with Video:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ ▶ {videoUrl.Substring(0, Math.Min(20, videoUrl.Length))}... ║");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine("╚══════════════════════════╝");
        }
    }

    // ========== ABSTRACTION (Bridge) - Tipos de notificação ==========
    public abstract class Notification
    {
        protected readonly INotificationRenderer _renderer;

        protected Notification(INotificationRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Render();
    }

    public class TextNotification : Notification
    {
        private readonly string _title;
        private readonly string _content;

        public TextNotification(INotificationRenderer renderer, string title, string content)
            : base(renderer)
        {
            _title = title;
            _content = content;
        }

        public override void Render() => _renderer.RenderText(_title, _content);
    }

    public class ImageNotification : Notification
    {
        private readonly string _title;
        private readonly string _content;
        private readonly string _imageUrl;

        public ImageNotification(INotificationRenderer renderer, string title, string content, string imageUrl)
            : base(renderer)
        {
            _title = title;
            _content = content;
            _imageUrl = imageUrl;
        }

        public override void Render() => _renderer.RenderImage(_title, _content, _imageUrl);
    }

    public class VideoNotification : Notification
    {
        private readonly string _title;
        private readonly string _content;
        private readonly string _videoUrl;

        public VideoNotification(INotificationRenderer renderer, string title, string content, string videoUrl)
            : base(renderer)
        {
            _title = title;
            _content = content;
            _videoUrl = videoUrl;
        }

        public override void Render() => _renderer.RenderVideo(_title, _content, _videoUrl);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações (Bridge Pattern) ===\n");

            // Combina qualquer tipo com qualquer plataforma - sem explosão de classes!
            var webRenderer = new WebRenderer();
            var mobileRenderer = new MobileRenderer();
            var desktopRenderer = new DesktopRenderer();

            // Texto em diferentes plataformas
            var textWeb = new TextNotification(webRenderer, "Novo Pedido", "Você tem um novo pedido");
            textWeb.Render();
            Console.WriteLine();

            var textMobile = new TextNotification(mobileRenderer, "Novo Pedido", "Você tem um novo pedido");
            textMobile.Render();
            Console.WriteLine();

            // Imagem em Web
            var imageWeb = new ImageNotification(webRenderer, "Promoção", "50% de desconto!", "promo.jpg");
            imageWeb.Render();
            Console.WriteLine();

            // Vídeo em Mobile
            var videoMobile = new VideoNotification(mobileRenderer, "Tutorial", "Aprenda a usar o app", "tutorial.mp4");
            videoMobile.Render();
            Console.WriteLine();

            // Novas combinações sem criar novas classes!
            var imageDesktop = new ImageNotification(desktopRenderer, "Alerta", "Verifique sua conta", "alert.png");
            imageDesktop.Render();

        }
    }
}
