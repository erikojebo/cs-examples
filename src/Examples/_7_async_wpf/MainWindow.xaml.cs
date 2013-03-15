using System;
using System.Net.Http;
using System.Windows;

namespace _7_async_wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CalculateAsyncTplOnlyCorrect();
        }

        private void Calculate()
        {
            ProgressBar.IsIndeterminate = true;

            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync("http://www.google.com");
                var result = task.Result;

                ResultTextBlock.Text = result;
            }

            ProgressBar.IsIndeterminate = false;
        }

        private async void CalculateAsync()
        {
            ProgressBar.IsIndeterminate = true;

            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync("http://www.google.com");
                var result = await task;

                ResultTextBlock.Text = result;
            }

            ProgressBar.IsIndeterminate = false;
        }

        private void CalculateAsyncTplOnlyWithBugs()
        {
            ProgressBar.IsIndeterminate = true;

            using (var client = new HttpClient())
            {
                client.GetStringAsync("http://www.google.com")
                      .ContinueWith(t => { ResultTextBlock.Text = t.Result; });

                ProgressBar.IsIndeterminate = false;
            }
        }

        private void CalculateAsyncTplOnlyCorrect()
        {
            ProgressBar.IsIndeterminate = true;

            var client = new HttpClient();
            client.GetStringAsync("http://www.google.com")
                  .ContinueWith(t =>
                      {
                          Application.Current.Dispatcher.BeginInvoke((Action)(() => ResultTextBlock.Text = t.Result));
                          client.Dispose();
                          ProgressBar.IsIndeterminate = false;
                      });
        }
    }
}