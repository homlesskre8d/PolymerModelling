using ChemModel.Data;
using ChemModel.Data.DbTables;
using ChemModel.Messeges;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ChemModel.ViewModels
{
    public partial class AdminViewModel : ObservableObject, ICloseWindow.ICloseWindows
    {
        [ObservableProperty]
        private bool recoverState;
        [ObservableProperty]
        private bool copyState;
        [ObservableProperty]
        private string mem = "";

        [ObservableProperty] private string lastSave;

        private DispatcherTimer timer;
        public AdminViewModel()
        {
            timer = new();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (e, args) =>
            {
                using Process proc = Process.GetCurrentProcess();
                Mem = Math.Round((double)(proc.PrivateMemorySize64 / (1024 * 1024)), 2) + " МБ";
            };
            timer.Start();
        }
        [RelayCommand]
        private async void Copy()
        {
            var test = "";
            List<Material> qwe = new List<Material>();
            using (Context ctx = new Context() )
            {
                qwe = ctx.Materials.Include(m => m.MathModelProperties).Include(m => m.Properties).ToList();
                ctx.SaveChanges();

            }
            await Task.Run(() => CopyChange());

            if (DBConfig.Destination == @"qwe.db")
            {
              
                File.Copy(DBConfig.Destination, "qweS.db", true);
                test = "qweS.db";
            }
            else
            {
               
                File.Copy(DBConfig.Destination, "qwe.db", true);
                test = "qwe.db";


            }

            DBConfig.LastSave = DateTime.Now.ToString();
           
            DBConfig.RealSave = true;
            using (Context ctx = new Context(test))
            {
                var t = ctx.Materials.Include(m => m.MathModelProperties).Include(m => m.Properties).ToList();
                ctx.Materials.RemoveRange(t);
                ctx.Materials.AddRange(qwe);
                ctx.SaveChanges();

            }
        }

        private async Task RecoverChange()
        {
            RecoverState = true;
            Thread.Sleep(2000);
            RecoverState = false;

        }
        private async Task CopyChange()
        {
            CopyState = true;
            Thread.Sleep(2000);
            CopyState = false;

        }

        [RelayCommand]
        private async void Recover()
        {
            await Task.Run(() => RecoverChange());

            if (!DBConfig.RealSave)
                return;

            await Task.Run(() => RecoverChange());
            if (DBConfig.Destination == @"qwe.db")
            {
                DBConfig.Destination = @"qweS.db";
              

            }
            else
            {
                DBConfig.Destination = @"qwe.db";
              


            }
            WeakReferenceMessenger.Default.Send(new ChangeDbMEssage(new ()));
            DBConfig.RealSave = false;



        }
        [RelayCommand]
        private void Logout()
        {
            var window = new AuthWindow();
            window.Show();
            Close.Invoke();
        }

        public Action Close { get; set; }
        public bool CanClose()
        {
            return true;
        }
    }
}
