using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WaveManagerUI;
using System.Threading;
using Microsoft.VisualBasic.ApplicationServices;

namespace WaveManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.Sleep(1000);
            new SplashScreenApp().Run(args);
        }
    }

    // adapted from: http://stackoverflow.com/questions/4364894/display-start-up-picture
    public class SplashScreenApp : WindowsFormsApplicationBase
    {
        protected override void OnCreateSplashScreen()
        {
            this.SplashScreen = new SplashForm();
            this.SplashScreen.ShowInTaskbar = false;
            this.SplashScreen.Cursor = Cursors.AppStarting;
        }

        protected override void OnCreateMainForm()
        {
            //FOR TESTING PURPOSES ONLY (remove once you've added your code)
            System.Threading.Thread.Sleep(1000);

            //Set the main form to a new instance of your form
            //(this will automatically close the splash screen)
            this.MainForm = new MdiMainForm();
        }
    }
}
