namespace OOP_Curs_Kanevskii_23VP2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (HelloForm helloForm = new HelloForm())
            {
                // Создаем таймер для автоматического перехода через 10 секунд
                System.Windows.Forms.Timer autoCloseTimer = new System.Windows.Forms.Timer();
                autoCloseTimer.Interval = 10000; // 10 секунд
                autoCloseTimer.Tick += (sender, e) =>
                {
                    autoCloseTimer.Stop();
                    helloForm.DialogResult = DialogResult.OK; // Имитируем нажатие кнопки
                };
                autoCloseTimer.Start();

                // Показываем форму и ждем либо таймера, либо нажатия кнопки
                if (helloForm.ShowDialog() == DialogResult.OK)
                {
                    autoCloseTimer.Stop(); // Останавливаем таймер, если переход по кнопке
                    Application.Run(new MainForm());
                }
            }
        }
    }
}