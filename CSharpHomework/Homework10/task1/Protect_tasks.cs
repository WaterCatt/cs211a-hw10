using System.Timers;

namespace Homework10
{
    class Protect_tasks
    {
        public static void Protected_on()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Включение оберегов от тестировшиков... ");
            ProgressBar progressBar = new ProgressBar();
            progressBar.Start();
            double perc_now = 0;
            while (perc_now < 99.9999)
            {
                perc_now += 5.65;
                progressBar.Update(5.65);
                Thread.Sleep(300);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Защита успешно включена");
            Console.ResetColor();
        }
    }

    class ProgressBar
    {
        System.Timers.Timer aTimer;
        double percent = 0;
        string white_space = new string(' ', 45);
        const string sleshs = @"|/-\";
        short ind_slesh = 0;
        (int Left, int Top) CurrentCursor;

        public ProgressBar() { }

        public void Start()
        {
            Console.Write("\b" + white_space);
            CurrentCursor = Console.GetCursorPosition();
            CurrentCursor.Left -= 39;
            SetTimer();
        }

        public void Update(double val)
        {
            percent += val;
            if (percent > 99.99)
            {
                ProgressEnd();
            }
        }

        private void ReWrite()
        {
            Console.SetCursorPosition(CurrentCursor.Left, CurrentCursor.Top);

            Console.BackgroundColor = ConsoleColor.Red;
            int percent_int = (int)percent / 4;
            Console.Write(white_space[..percent_int]);
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Write($"{white_space[..(24 - percent_int)]}   {string.Format("{0,6:N1}", percent)}%    {sleshs[ind_slesh]}");
            ind_slesh++;
            ind_slesh %= 4;
        }

        private void ProgressEnd()
        {
            aTimer.Stop();
            aTimer.Dispose();
            Console.CursorLeft = Console.GetCursorPosition().Left - 11;
            Console.Write($"100.0%{white_space[..5]}\n");
        }

        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(200);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            ReWrite();
        }
    }
}
