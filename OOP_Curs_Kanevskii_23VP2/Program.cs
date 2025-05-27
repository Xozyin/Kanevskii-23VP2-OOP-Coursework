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
                // ������� ������ ��� ��������������� �������� ����� 10 ������
                System.Windows.Forms.Timer autoCloseTimer = new System.Windows.Forms.Timer();
                autoCloseTimer.Interval = 10000; // 10 ������
                autoCloseTimer.Tick += (sender, e) =>
                {
                    autoCloseTimer.Stop();
                    helloForm.DialogResult = DialogResult.OK; // ��������� ������� ������
                };
                autoCloseTimer.Start();

                // ���������� ����� � ���� ���� �������, ���� ������� ������
                if (helloForm.ShowDialog() == DialogResult.OK)
                {
                    autoCloseTimer.Stop(); // ������������� ������, ���� ������� �� ������
                    Application.Run(new MainForm());
                }
            }
        }
    }
}